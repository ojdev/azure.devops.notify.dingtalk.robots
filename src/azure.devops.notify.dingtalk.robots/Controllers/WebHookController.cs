using azure.devops.notify.dingtalk.robots.AzureDevOps;
using azure.devops.notify.dingtalk.robots.Dtos;
using azure.devops.notify.dingtalk.robots.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace azure.devops.notify.dingtalk.robots.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WebHookController : ControllerBase
    {
        private readonly IDingTalkService _dingTalkService;
        private readonly ILogger<WebHookController> _logger;
        private readonly AppSettings _settings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dingTalkService"></param>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        public WebHookController(IDingTalkService dingTalkService, ILogger<WebHookController> logger, AppSettings settings)
        {
            _dingTalkService = dingTalkService ?? throw new ArgumentNullException(nameof(dingTalkService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string RequestUpdatedStatus(string status) => status switch
        {
            "completed" => "已完成",
            "active" => "激活",
            "abandoned" => "已放弃",
            _ => status,
        };
        public string ReviewStatus(int vote) => vote switch
        {
            -10 => " [拒绝]",
            -5 => " [无聊]",
            5 => " [会议]",
            10 => " [OK]",
            _ => ""
        };
        /// <summary>
        /// 发布部署等待审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReleaseUpdated([FromBody] WebHooksRequestDeployCreatedResource request)
        {
            string html = request.Resource?.Deployment?.ReleaseDefinition?.GetWebUrl;
            string repository = request.Resource?.Deployment?.ReleaseDefinition?.Name;

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"#### [#{request.Resource?.Deployment?.Release?.Name} {repository} 触发自动部署]({html})");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"> 发起: @{request.Resource?.Deployment?.RequestedFor?.DisplayName}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 阶段: {request.Resource?.Environment?.Name}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 状态: {request.Resource?.Environment?.Status}");
            stringBuilder.AppendLine("> ");

            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(request.Message.MarkDown);
            await _dingTalkService.MarkdownAsync("CICD", "CD", $"{repository} 触发自动部署", stringBuilder.ToString());
            return Ok();
        }
        /// <summary>
        /// 拉取请求
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RequestUpdated([FromBody] WebHooksRequestPullRequestUpdatedResource request)
        {
            var createdBy = request.Resource.CreatedBy.DisplayName;
            string html = request.Resource.Links.GetValueOrDefault("web")?.Href;
            string repository = request.Resource.Repository.Name;

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"#### [#{request.Resource.CodeReviewId} {repository} 产生了 {request.Resource.SourceRefName.Replace("refs/heads/", "")} 到 {request.Resource.TargetRefName.Replace("refs/heads/", "")} 的拉取请求]({html})");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"> 仓储: {repository}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 创建: @{createdBy}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 合并: {request.Resource.SourceRefName.Replace("refs/heads/", "")} 到 {request.Resource.TargetRefName.Replace("refs/heads/", "")} {(request.Resource.MergeStatus == "conflicts" ? "[打叉]合并冲突" : "")}");
            stringBuilder.AppendLine("> ");


            //request.Resource.Status->completed,active,abandoned
            stringBuilder.AppendLine($"> 状态: {RequestUpdatedStatus(request.Resource.Status)}");
            stringBuilder.AppendLine("> ");
            if (request.Resource.Reviewers.Any())
            {
                stringBuilder.AppendLine("---");
                var requiredreviews = request.Resource.Reviewers?.Where(t => t.IsRequired == true)?.Select(t => $"@{t.DisplayName}{ReviewStatus(t.Vote)}").ToList() ?? new List<string>();
                if (requiredreviews.Any())
                {
                    stringBuilder.AppendLine($"> 审阅-必须: {string.Join(" 、", requiredreviews)}");
                    stringBuilder.AppendLine("> ");
                }
                var reviews = request.Resource.Reviewers?.Where(t => t.IsRequired != true)?.Select(t => $"@{t.DisplayName}{ReviewStatus(t.Vote)}").ToList() ?? new List<string>();
                if (reviews.Any())
                {
                    stringBuilder.AppendLine($"> 审阅-可选: {string.Join(" 、", reviews)}");
                    stringBuilder.AppendLine("> ");
                }
            }
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(request.DetailedMessage.MarkDown);
            await _dingTalkService.MarkdownAsync("PR", "", request.Resource.Title, stringBuilder.ToString());
            return Ok();
        }
        /// <summary>
        /// 工作项更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WorkitemUpdated([FromBody] WebHooksRequestInputDto request)
        {
            if (request.Resource.Relations != null)
            {
                return Ok();
            }

            var workItemId = request.Resource.WorkItemId;
            var workItemType = request.Resource.Revision.Fields.GetValueOrDefault("System.WorkItemType");
            var state = request.Resource.Revision.Fields.GetValueOrDefault("System.State");
            var reason = request.Resource.Revision.Fields.GetValueOrDefault("System.Reason");
            var createdBy = request.Resource.Revision.Fields.GetValueOrDefault("System.CreatedBy");
            var assignedTo = request.Resource.Revision.Fields.GetValueOrDefault("System.AssignedTo");
            var revisedBy = request.Resource.RevisedBy.DisplayName;
            var title = request.Resource.Revision.Fields.GetValueOrDefault("System.Title")?.ToString();
            string html = request.Resource.Links.GetValueOrDefault("html")?.Href;
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"#### [{workItemType}] #{workItemId} {state} [{title}]({html})");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"> 创建: @{createdBy?.ToString()?.Split(' ')?[0]}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"> 修改: {revisedBy?.ToString()?.Split(' ')?[0]}");
            stringBuilder.AppendLine();

            if (assignedTo != null)
            {
                stringBuilder.AppendLine($"> 指派: @{assignedTo?.ToString()?.Split(' ')?[0]}");
                stringBuilder.AppendLine();
            }

            var description = request.Resource.Revision.Fields.GetValueOrDefault("System.Description");
            if (description != null)
            {
                stringBuilder.AppendLine("---");
                stringBuilder.AppendLine($"> 描述");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{Regex.Replace(Regex.Replace(description.ToString(), "<[^>]+>", ""), "&[^;]+;", "")}");
                stringBuilder.AppendLine();
            }

            var history = request.Resource.Revision.Fields.GetValueOrDefault("System.History");
            if (history != null)
            {
                var strNohtml = Regex.Replace(Regex.Replace(history.ToString(), "<[^>]+>", ""), "&[^;]+;", "");
                stringBuilder.AppendLine("---");
                stringBuilder.AppendLine($"> {revisedBy?.ToString()?.Split(' ')?[0]} 写了讨论");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{strNohtml}");
                stringBuilder.AppendLine();
            }

            var changeStatus = request.Resource.Fields.GetValueOrDefault("System.State");
            var changeAssignedTo = request.Resource.Fields.GetValueOrDefault("System.AssignedTo");
            var priority = request.Resource.Fields.GetValueOrDefault("Microsoft.VSTS.Common.Priority");
            var remainingWork = request.Resource.Fields.GetValueOrDefault("Microsoft.VSTS.Scheduling.RemainingWork");
            var originalEstimate = request.Resource.Fields.GetValueOrDefault("Microsoft.VSTS.Scheduling.OriginalEstimate");
            if (priority != null || remainingWork != null || originalEstimate != null)
            {
                stringBuilder.AppendLine("---");
                if (priority != null)
                {
                    try
                    {

                        var ch = priority.ToValueChangeModel();
                        if (ch.OldValue != ch.NewValue)
                        {
                            stringBuilder.AppendLine($"> 优先级: {ch.OldValue} 更改为 {ch.NewValue}");
                            stringBuilder.AppendLine();
                        }
                    }
                    catch
                    {
                    }
                }
                if (changeStatus != null)
                {
                    try
                    {
                        var ch = changeStatus.ToValueChangeModel();
                        if (ch.OldValue != ch.NewValue)
                        {
                            stringBuilder.AppendLine($"> 状态变更: {ch.OldValue} 更改为 {ch.NewValue}");
                            stringBuilder.AppendLine();
                        }
                    }
                    catch
                    {
                    }
                }
                if (changeAssignedTo != null)
                {
                    try
                    {
                        var ch = changeAssignedTo.ToValueChangeModel();
                        if (ch.OldValue != ch.NewValue)
                        {
                            stringBuilder.AppendLine($"> 指派变更: {$"{ch.OldValue}".Split(' ')?[0]} 更改为 @{$"{ch.NewValue}".Split(' ')?[0]}");
                            stringBuilder.AppendLine();
                        }
                    }
                    catch
                    {
                    }
                }
                if (remainingWork != null)
                {
                    try
                    {
                        var ch = remainingWork.ToValueChangeModel();
                        if (ch.OldValue != ch.NewValue)
                        {
                            stringBuilder.AppendLine($"> 剩余工作: {ch.OldValue} 更改为 {ch.NewValue}");
                            stringBuilder.AppendLine();
                        }
                    }
                    catch
                    {
                    }
                }
                if (originalEstimate != null)
                {
                    try
                    {
                        var ch = originalEstimate.ToValueChangeModel();
                        if (ch.OldValue != ch.NewValue)
                        {
                            stringBuilder.AppendLine($"> 初始估计: {ch.OldValue} 更改为 {ch.NewValue}");
                            stringBuilder.AppendLine();
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if (request.Resource.Fields.Keys.Any(x => _settings.WorkItemCustomNodes.Any(t => t.Key == x)))
            {
                stringBuilder.AppendLine("---");
                foreach (var setting in _settings.WorkItemCustomNodes)
                {
                    var node = request.Resource.Fields.GetValueOrDefault(setting.Key);
                    if (node != null)
                    {
                        try
                        {
                            var ch = node.ToValueChangeModel();
                            if (ch.OldValue != ch.NewValue)
                            {
                                var o = $"{ch.OldValue}";
                                var n = $"{ch.NewValue}";
                                if (!string.IsNullOrWhiteSpace(setting.Format))
                                {
                                    o = string.Format($"{{0:{setting.Format}}}", o);
                                    n = string.Format($"{{0:{setting.Format}}}", n);
                                }
                                stringBuilder.AppendLine($"> {setting.Name}: {o} 更改为 {n}");
                                stringBuilder.AppendLine();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            await _dingTalkService.MarkdownAsync("Task", $"{workItemType}", $"{workItemType} #{workItemId} {title} {reason}", stringBuilder.ToString());
            return Ok($"{workItemId} is ok!");
        }
    }
}
