using azure.devops.notify.dingtalk.robots.AzureDevOps;
using azure.devops.notify.dingtalk.robots.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> PullRequestCreated([FromBody] WebHooksRequestPullRequestCreatedResource request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown());
        }
        /// <summary>
        /// 拉取请求更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PullRequestUpdated([FromBody] WebHooksRequestPullRequestUpdatedResource request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown());
        }
        /// <summary>
        /// 工作项评论
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WorkitemCommented([FromBody] WebHooksRequestWorkitemCommentedResource request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown());
        }
        /// <summary>
        /// 工作项更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WorkitemUpdated([FromBody] WebHooksRequestWorkitemUpdatedResource request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
            return Ok(await _dingTalkService.Markdown());
        }
    }
}
