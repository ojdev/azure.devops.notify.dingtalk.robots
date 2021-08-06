using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    /// <summary>
    /// 工作项更新
    /// </summary>
    public class WorkitemUpdatedResource : Resource
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public int Rev { get; set; }
        public string Url { get; set; }
        public RevisedBy RevisedBy { get; set; }
        public DateTimeOffset? RevisedDate { get; set; }
        public Dictionary<string, ValueChangeModel> Fields { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, HrefModel> Links { get; set; }
        public Revision Revision { get; set; }
    }
}
