using System.Dynamic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Revision
    {
        public int Id { get; set; }
        public int Rev { get; set; }
        public ExpandoObject Fields { get; set; }
    }
}
