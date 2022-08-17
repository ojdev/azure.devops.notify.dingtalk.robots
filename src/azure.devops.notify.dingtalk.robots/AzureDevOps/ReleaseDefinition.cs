using Newtonsoft.Json;
using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class NameLinks
    {
        public string Name { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, HrefModel> Links { get; set; }
        public string GetWebUrl => Links.GetValueOrDefault("web")?.Href;
    }
}
