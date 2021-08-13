using System;

namespace azure.devops.notify.dingtalk.robots.Dtos
{
    public class UserMapping
    {
        public string DevOps { get; set; }
        public string DingTalk { get; set; }
        [Obsolete("暂未启用")]
        public bool AtOnly { get; set; } = false;
    }
}
