using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.AzureDevOps
{
    public class PullRequestCreatedResource : Resource
    {
        public Repository Repository { get; set; }
        public int PullRequestId { get; set; }
        public string Status { get; set; }
        public By CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SourceRefName { get; set; }
        public string TargetRefName { get; set; }
        public string MergeStatus { get; set; }
        public Guid MergeId { get; set; }
        public Commit LastMergeSourceCommit { get; set; }
        public Commit LastMergeTargetCommit { get; set; }
        public Commit LastMergeCommit { get; set; }
        public List<Reviewer> Reviewers { get; set; } = new List<Reviewer>();
        public List<Commit> Commits { get; set; }
        public string Url { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, HrefModel> Links { get; set; }
    }
}
