using System;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Repository
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Project Project { get; set; }
        public string DefaultBranch { get; set; }
        public string RemoteUrl { get; set; }
    }
}
