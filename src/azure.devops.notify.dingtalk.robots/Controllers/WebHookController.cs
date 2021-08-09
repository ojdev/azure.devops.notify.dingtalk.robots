using azure.devops.notify.dingtalk.robots.AzureDevOps;
using azure.devops.notify.dingtalk.robots.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace azure.devops.notify.dingtalk.robots.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WebHookController : ControllerBase
    {
        private readonly IDingTalkService _dingTalkService;
        private readonly ILogger<WebHookController> _logger;

        public WebHookController(IDingTalkService dingTalkService, ILogger<WebHookController> logger)
        {
            _dingTalkService = dingTalkService ?? throw new ArgumentNullException(nameof(dingTalkService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            stringBuilder.AppendLine($"#### [#{request.Resource.CodeReviewId} {createdBy} {request.Resource.SourceRefName.Replace("refs/heads/", "")} 到 {request.Resource.TargetRefName.Replace("refs/heads/", "")} 分支的拉取请求]({html})");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"> 仓储: {repository}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 合并: {request.Resource.SourceRefName.Replace("refs/heads/", "")} 到 {request.Resource.TargetRefName.Replace("refs/heads/", "")} {request.Resource.MergeStatus}");
            stringBuilder.AppendLine("> ");
            stringBuilder.AppendLine($"> 状态: {request.Resource.Status}");
            stringBuilder.AppendLine("> ");

            var reviews = request.Resource.Reviewers?.Select(t => $"@{t.displayName}").ToList() ?? new List<string>();
            if (reviews.Any())
            {
                stringBuilder.AppendLine($"> 审阅者: {string.Join(' ', reviews)}");
                stringBuilder.AppendLine("> ");
            }
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(request.DetailedMessage.MarkDown);
            await _dingTalkService.Markdown(request.Resource.Title, stringBuilder.ToString());
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

            var changeStatus = request.Resource.Fields.GetValueOrDefault("System.State");
            if (changeStatus != null)
            {
                try
                {
                    var ch = (ValueChangeModel)changeStatus;
                    if (ch.OldValue != ch.NewValue)
                    {
                        stringBuilder.AppendLine($"- 状态变更: {ch.OldValue} 更改为 {ch.NewValue}");
                        stringBuilder.AppendLine();
                    }
                }
                catch
                {
                }
            }
            var changeAssignedTo = request.Resource.Fields.GetValueOrDefault("System.AssignedTo");
            if (changeAssignedTo != null)
            {
                try
                {
                    var ch = (ValueChangeModel)changeAssignedTo;
                    if (ch.OldValue != ch.NewValue)
                    {
                        stringBuilder.AppendLine($"- 指派变更: {ch.OldValue} 更改为 @{ch.NewValue}");
                        stringBuilder.AppendLine();
                    }
                }
                catch
                {
                }
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
            await _dingTalkService.Markdown($"{workItemType} #{workItemId} {title} {reason}", stringBuilder.ToString());
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string title, string content, bool atAll = false)
        {
            await _dingTalkService.Markdown(title, content, atAll);
            return Ok();
        }
    }
}
