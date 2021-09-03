namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class Reviewer
    {
        public string ReviewerUrl { get; set; }
        public int Vote { get; set; }
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public bool? IsRequired { get; set; }

    }
}
