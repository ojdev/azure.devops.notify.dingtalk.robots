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
        /// 创建拉取请求
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PullRequestCreated([FromBody] ExpandoObject request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown("", ""));
        }
        /// <summary>
        /// 拉取请求更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PullRequestUpdated([FromBody] ExpandoObject request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown("", ""));
        }
        /// <summary>
        /// 工作项评论
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WorkitemCommented([FromBody] WebHooksRequestInputDto request)
        {
            //var resource = JsonConvert.DeserializeObject<SystemResourceModel>(request.FirstOrDefault(t => t.Key == "resource").Value.ToString());
            var workItemType = request.Resource.Fields["System.WorkItemType"];
            var state = request.Resource.Fields["System.State"];
            var assignedTo = request.Resource.Fields["System.AssignedTo"];
            var title = request.Resource.Fields["System.Title"].ToString();
            var history = request.Resource.Fields["System.History"];
            var changedDate = DateTime.Parse(request.Resource.Fields["System.ChangedDate"].ToString());
            var changedBy = request.Resource.Fields["System.ChangedBy"];
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(history.ToString(), "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");
            //string html = request.Resource.Links["html"].Href;
            var conent =
@$"
{workItemType} #{request.Resource.WorkItemId} {title}

---

@{changedBy} {changedDate:yyyy-MM-dd HH:MM} 说:

{StrNohtml}

---

";
            await _dingTalkService.Markdown(title, conent);
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
            var workItemId = request.Resource.WorkItemId;
            var workItemType = request.Resource.Revision.Fields["System.WorkItemType"];
            var state = request.Resource.Revision.Fields["System.State"];
            var reason = request.Resource.Revision.Fields["System.Reason"];
            var createdBy = request.Resource.Revision.Fields["System.CreatedBy"];
            var assignedTo = request.Resource.Revision.Fields["System.AssignedTo"];
            var revisedBy = request.Resource.RevisedBy.DisplayName;
            var title = request.Resource.Revision.Fields["System.Title"].ToString();
            string html = request.Resource.Links["html"].Href;

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"#### [{workItemType}] #{workItemId} {state} [{title}]({html})");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"> 创建: {createdBy?.ToString()?.Split(' ')?[0]}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"> 修改: {revisedBy?.ToString()?.Split(' ')?[0]}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"> 指派: {assignedTo?.ToString()?.Split(' ')?[0]}");
            stringBuilder.AppendLine();

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
    }
}
