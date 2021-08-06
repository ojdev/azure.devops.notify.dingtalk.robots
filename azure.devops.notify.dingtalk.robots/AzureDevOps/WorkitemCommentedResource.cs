using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    /// <summary>
    /// 工作项评论
    /// </summary>
    public class WorkitemCommentedResource : Resource
    {
        public int Id { get; set; }
        public int Rev { get; set; }
        public ExpandoObject Fields { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, HrefModel> Links { get; set; }
        public string Url { get; set; }
    }
}
