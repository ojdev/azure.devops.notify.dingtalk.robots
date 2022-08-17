namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class WebHooksRequestResourceRequest
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public By RequestedFor { get; set; }
    }
}
