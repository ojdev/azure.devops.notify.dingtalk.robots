using azure.devops.notify.dingtalk.robots.Dtos;
using DingTalk.Api;
using DingTalk.Api.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top.Api.Util;

namespace azure.devops.notify.dingtalk.robots.Infrastructure
{
    public interface IDingTalkService
    {
        Task MarkdownAsync(string type, string workItemType, string title, string content, bool atAll = false);
        [Obsolete("效果不是太好，会在钉钉内部打开侧边，不适合")]
        void ActionCard(string title, string content, string workItemUrl);
    }
    public class DingTalkService : IDingTalkService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<IDingTalkService> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="logger"></param>
        public DingTalkService(AppSettings appSettings, ILogger<IDingTalkService> logger)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="workItemUrl"></param>
        [Obsolete("效果不是太好，会在钉钉内部打开侧边，不适合")]
        public void ActionCard(string title, string content, string workItemUrl)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var allUsers = _appSettings.DefaultUserMappings.Select(t => new { t.DevOps, t.DingTalk }).Distinct();
            foreach (var g in _appSettings.Robots.GroupBy(t => new { t.Access_Token, t.Secret }))
            {
                var signMsg = timestamp + "\n" + g.Key.Secret;
                var sign = DingTalkSignatureUtil.ComputeSignature(g.Key.Secret, signMsg);
                var url = $"https://oapi.dingtalk.com/robot/send?access_token={g.Key.Access_Token}&timestamp={timestamp}&sign={sign}";
                IDingTalkClient client = new DefaultDingTalkClient(url);
                OapiRobotSendRequest request = new() { Msgtype = "actionCard" };
                List<string> atPhones = new();
                foreach (var d in allUsers)
                {
                    if (content.Contains($"@{d.DevOps}"))
                    {
                        content = content.Replace($"@{d.DevOps}", $" @{d.DingTalk} ");
                        atPhones.Add(d.DingTalk);
                    }
                }
                //content = content;
                request.ActionCard_ = new()
                {
                    Title = title,
                    Text = content,
                    BtnOrientation = "1",
                    Btns = new List<OapiRobotSendRequest.BtnsDomain>
                    {
                        new OapiRobotSendRequest.BtnsDomain
                        {
                            Title="去看看",
                            ActionURL=workItemUrl.Replace("_apis/wit/workItems/", "_workitems/edit/")
                        }
                    },
                    SingleTitle = "去处理",
                    SingleURL = workItemUrl.Replace("_apis/wit/workItems/", "_workitems/edit/")
                };
                request.At_ = new()
                {
                    AtMobiles = atPhones,
                    IsAtAll = false
                };
                var response = client.Execute(request);
                _logger.LogInformation(response.Body);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="workItemType"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="atAll"></param>
        /// <returns></returns>
        public async Task MarkdownAsync(string type, string workItemType, string title, string content, bool atAll = false)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            foreach (var g in _appSettings.Robots.Where(t => t.Types.Any(x => x.Type == type && (!x.WorkItemTypes.Any() || x.WorkItemTypes.Contains(workItemType)))).Distinct())
            {
                var signMsg = timestamp + "\n" + g.Secret;
                var sign = DingTalkSignatureUtil.ComputeSignature(g.Secret, signMsg);
                var url = $"https://oapi.dingtalk.com/robot/send?access_token={g.Access_Token}&timestamp={timestamp}&sign={sign}";
                IDingTalkClient client = new DefaultDingTalkClient(url);
                OapiRobotSendRequest request = new() { Msgtype = "markdown" };
                List<string> at = new();
                var ddTalks = (g.UserMappings.Any() ? g.UserMappings : _appSettings.DefaultUserMappings).AsQueryable();
                if (g.AtOnly)
                {
                    ddTalks = ddTalks.Where(t => content.Contains(t.DevOps));
                }
                var sender = !g.AtOnly || ddTalks.Any();
                if (sender)
                {
                    foreach (var d in ddTalks)
                    {
                        if (content.Contains($"@{d.DevOps}"))
                        {
                            content = content.Replace($"@{d.DevOps}", $" @{d.DingTalk} ");
                            at.Add(d.DingTalk);
                        }
                    }
                    request.Markdown_ = new()
                    {
                        Title = title,
                        Text = content
                    };
                    request.At_ = new()
                    {
                        AtMobiles = at,
                        IsAtAll = !g.AtOnly
                    };
                    var response = client.Execute(request);
                    await Task.Yield();
                    _logger.LogInformation(response.Body);
                }
            }
        }
    }
}
