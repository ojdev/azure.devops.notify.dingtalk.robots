namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class DeployReauestCreatedResource : Resource
    {
        public Environment Environment { get; set; }
        public Deployment Deployment { get; set; }
        public string Url { get; set; }
    }
}
