namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Deployment
    {
        public ReleaseDefinition ReleaseDefinition { get; set; }
        public string DeploymentStatus { get; set; }

        public By RequestedFor { get; set; }
    }
}
