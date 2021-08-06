using System;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class RequestedFor
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public Guid Id { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
    }
}
