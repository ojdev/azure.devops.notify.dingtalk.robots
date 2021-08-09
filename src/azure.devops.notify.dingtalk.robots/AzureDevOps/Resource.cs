using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Resource
    {
        //public string Uri { get; set; }
        //public int Id { get; set; }
        //public string BuildNumber { get; set; }
        //public string Url { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? FinishTime { get; set; }
        //public string Reason { get; set; }
        //public string Status { get; set; }
        //public string DropLocation { get; set; }
        //public WebHooksRequestResourceDrop Drop { get; set; }
        //public WebHooksRequestResourceLog Log { get; set; }
        //public string SourceGetVersion { get; set; }
        //public By LastChangedBy { get; set; }
        //public bool RetainIndefinitely { get; set; }
        //public bool HasDiagnostics { get; set; }
        //public WebHooksRequestResourceDefinition Definition { get; set; }
        //public WebHooksRequestResourceQueue Queue { get; set; }
        //public List<WebHooksRequestResourceRequest> Requests { get; set; }
    }
    public class SystemResourceModel
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public int Rev { get; set; }
        public RevisedBy RevisedBy { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, HrefModel> Links { get; set; }
        public SystemResourceModel Revision { get; set; }
        public string Url { get; set; }
    }
}
