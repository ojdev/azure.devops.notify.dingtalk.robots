using System;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class PullRequestUpdatedResource : PullRequestCreatedResource
    {
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
    }
}
