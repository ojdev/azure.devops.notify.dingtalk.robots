using Newtonsoft.Json;
using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Deployment
    {
        public NameLinks ReleaseDefinition { get; set; }
        public NameLinks Release { get; set; }
        public string DeploymentStatus { get; set; }

        public By RequestedFor { get; set; }
    }
}
