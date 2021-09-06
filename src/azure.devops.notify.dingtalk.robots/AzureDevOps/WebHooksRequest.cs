using System;
using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class WebHooksRequestPullRequestCreatedResource : WebHooksRequest<PullRequestCreatedResource>
    {
    }
    public class WebHooksRequestPullRequestUpdatedResource : WebHooksRequest<PullRequestUpdatedResource>
    {
    }
    public class WebHooksRequestWorkitemUpdatedResource : WebHooksRequest<WorkitemUpdatedResource>
    {
    }
    public class ReleaseUpdateInputDto
    {
        public DetailedMessage DetailedMessage { get; set; }
    }
    public class WebHooksRequestInputDto
    {
        public string SubscriptionId { get; set; }
        public int NotificationId { get; set; }
        public string Id { get; set; }
        public string EventType { get; set; }
        public string PublisherId { get; set; }
        public Message Message { get; set; }
        public DetailedMessage DetailedMessage { get; set; }
        public SystemResourceModel Resource { get; set; }
        public string ResourceVersion { get; set; }
        public Dictionary<string, IdModel> ResourceContainers { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class WebHooksRequest<T> where T : Resource
    {
        public string SubscriptionId { get; set; }
        public int NotificationId { get; set; }
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string PublisherId { get; set; }
        public Message Message { get; set; }
        public DetailedMessage DetailedMessage { get; set; }

        public T Resource { get; set; }
        public string ResourceVersion { get; set; }
        public Dictionary<string, IdModel> ResourceContainers { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
