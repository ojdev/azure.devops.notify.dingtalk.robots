using System;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public string Visibility { get; set; }
        public DateTimeOffset? LastUpdateTime { get; set; }
    }
}
