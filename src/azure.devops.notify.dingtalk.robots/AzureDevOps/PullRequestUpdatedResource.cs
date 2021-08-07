using System;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class PullRequestUpdatedResource : PullRequestCreatedResource
    {
        public DateTime? CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}
