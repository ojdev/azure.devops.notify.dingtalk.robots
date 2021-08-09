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
            string html = request.Resource.Links["html"].Href;
            var conent =
@$"
[{workItemType}] #{request.Resource.Id} {title}

---

@{changedBy} {changedDate:yyyy-MM-dd HH:MM} 说:

{StrNohtml}

---

[去处理]({html})
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
        public async Task<IActionResult> WorkitemUpdated([FromBody] ExpandoObject request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown("", ""));
        }
    }
}
