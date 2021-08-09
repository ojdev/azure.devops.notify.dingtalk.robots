using azure.devops.notify.dingtalk.robots.Dtos;
using DingTalk.Api;
using DingTalk.Api.Request;
using Microsoft.Extensions.Configuration;
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
        Task<string> Markdown(string title, string content, bool atAll = false);
        void ActionCard(string title, string content, string workItemUrl);
    }
    public class DingTalkService : IDingTalkService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IDingTalkService> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public DingTalkService(IConfiguration configuration, ILogger<IDingTalkService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IEnumerable<Roboot> Roboots
        {
            get
            {
                var mappings = _configuration.GetSection("reboots").GetChildren().Select(c => new Roboot
                {
                    Name = c["Name"],
                    Access_Token = c["access_token"],
                    Secret = c["secret"]
                });
                return mappings;
            }
        }
        private IEnumerable<UserMapping> UserMappings
        {
            get
            {
                var mappings = _configuration.GetSection("UserMappings").GetChildren().Select(c => new UserMapping
                {
                    DevOps = c["devops"],
                    DingTalk = c["dingtalk"]
                });
                return mappings;
            }
        }

        public void ActionCard(string title, string content, string workItemUrl)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var allUsers = UserMappings.Select(t => new { t.DevOps, t.DingTalk }).Distinct();
            foreach (var g in Roboots.GroupBy(t => new { t.Access_Token, t.Secret }))
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

        public async Task<string> Markdown(string title, string content, bool atAll = false)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var allUsers = UserMappings.Select(t => new { t.DevOps, t.DingTalk }).Distinct();
            foreach (var g in Roboots.GroupBy(t => new { t.Access_Token, t.Secret }))
            {
                var signMsg = timestamp + "\n" + g.Key.Secret;
                var sign = DingTalkSignatureUtil.ComputeSignature(g.Key.Secret, signMsg);
                var url = $"https://oapi.dingtalk.com/robot/send?access_token={g.Key.Access_Token}&timestamp={timestamp}&sign={sign}";
                IDingTalkClient client = new DefaultDingTalkClient(url);
                OapiRobotSendRequest request = new() { Msgtype = "markdown" };
                List<string> atPhones = new();
                foreach (var d in allUsers)
                {
                    if (content.Contains($"@{d.DevOps}"))
                    {
                        content = content.Replace($"@{d.DevOps}", $" @{d.DingTalk} ");
                        atPhones.Add(d.DingTalk);
                    }
                }
                request.Markdown_ = new()
                {
                    Title = title,
                    Text = content
                };
                request.At_ = new()
                {
                    AtMobiles = atPhones,
                    IsAtAll = atAll
                };
                var response = client.Execute(request);
                _logger.LogInformation(response.Body);
            }
            return await Task.FromResult("");
        }
    }
}
