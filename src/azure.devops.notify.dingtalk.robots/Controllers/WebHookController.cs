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
            /* bug 的评论
             {
    "SubscriptionId": "ea7bd3d2-94e8-4dd7-9703-d7524beea45d",
    "NotificationId": 1,
    "Id": "4c19e2ca-d27c-4ed4-a69f-37ab2f2a95c7",
    "EventType": "workitem.commented",
    "PublisherId": "tfs",
    "Message": {
        "Text": "Bug #13 (创建一个bug) commented on by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)",
        "Html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) commented on by 欧俊",
        "MarkDown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) commented on by 欧俊"
    },
    "DetailedMessage": {
        "Text": "Bug #13 (创建一个bug) commented on by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)\r\n\r\n评论\r\n",
        "Html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) commented on by 欧俊<br/><div>评论</div>",
        "MarkDown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) commented on by 欧俊\r\n\r\n评论\r\n"
    },
    "Resource": {
        "Id": 13,
        "Rev": 3,
        "Fields": {
            "System.AreaPath": {
                "ValueKind": 3
            },
            "System.TeamProject": {
                "ValueKind": 3
            },
            "System.IterationPath": {
                "ValueKind": 3
            },
            "System.WorkItemType": {
                "ValueKind": 3
            },
            "System.State": {
                "ValueKind": 3
            },
            "System.Reason": {
                "ValueKind": 3
            },
            "System.AssignedTo": {
                "ValueKind": 3
            },
            "System.CreatedDate": {
                "ValueKind": 3
            },
            "System.CreatedBy": {
                "ValueKind": 3
            },
            "System.ChangedDate": {
                "ValueKind": 3
            },
            "System.ChangedBy": {
                "ValueKind": 3
            },
            "System.CommentCount": {
                "ValueKind": 4
            },
            "System.Title": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.Common.Severity": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.Common.StateChangeDate": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.Common.Priority": {
                "ValueKind": 4
            },
            "Microsoft.VSTS.Common.ValueArea": {
                "ValueKind": 3
            },
            "System.History": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.TCM.SystemInfo": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.TCM.ReproSteps": {
                "ValueKind": 3
            },
            "Microsoft.VSTS.Common.AcceptanceCriteria": {
                "ValueKind": 3
            }
        },
        "_links": null,
        "Url": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13"
    },
    "ResourceVersion": "1.0",
    "ResourceContainers": {
        "collection": {
            "Id": "2245531e-4735-4f13-81d1-a4a33d5532a1"
        },
        "account": {
            "Id": "0da6820a-0c82-40bd-8b31-bc1bb2a21a35"
        },
        "project": {
            "Id": "66776c68-10ec-4679-9652-d001a718d37f"
        }
    },
    "CreatedDate": "2021-08-07T15:42:14.8870739Z"
}
            */


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
