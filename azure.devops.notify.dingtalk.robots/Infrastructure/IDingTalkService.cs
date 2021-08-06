using azure.devops.notify.dingtalk.robots.Dtos;
using DingTalk.Api;
using DingTalk.Api.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Top.Api.Util;

namespace azure.devops.notify.dingtalk.robots.Infrastructure
{
    public interface IDingTalkService
    {
        Task<string> Markdown();
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

        private IEnumerable<DevOpsMappingDingTalk> GetMappings()
        {
            var mappings = _configuration.GetSection("Mappings").GetChildren().Select(c => new DevOpsMappingDingTalk
            {
                DevOps = c["devops"],
                DingTalk = c["dingtalk"],
                Access_Token = c["access_token"],
                Secret = c["secret"]
            });
            return mappings;
        }
        public async Task<string> Markdown()
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            foreach (var g in GetMappings().GroupBy(t => new { t.Access_Token, t.Secret }))
            {
                var signMsg = timestamp + "\n" + g.Key.Secret;
                var sign = DingTalkSignatureUtil.ComputeSignature(g.Key.Secret, signMsg);
                var url = $"https://oapi.dingtalk.com/robot/send?access_token={g.Key.Access_Token}&timestamp={timestamp}&sign={sign}";
                IDingTalkClient client = new DefaultDingTalkClient(url);
                OapiRobotSendRequest request = new();
                request.Msgtype = "markdown";
                OapiRobotSendRequest.MarkdownDomain md = new()
                {
                    Title = "标题",
                    Text = "内容"
                };
                request.Markdown_ = md;
                OapiRobotSendRequest.AtDomain at = new()
                {
                    AtMobiles = g.Select(t => t.DingTalk).Distinct().ToList(),
                    IsAtAll = false
                };
                request.At_ = at;
                var response = client.Execute(request);
                _logger.LogInformation(response.Body);
            }
            return await Task.FromResult("");
        }
    }
}
