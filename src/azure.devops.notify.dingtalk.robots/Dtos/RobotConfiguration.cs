﻿using System.Collections.Generic;

namespace azure.devops.notify.dingtalk.robots.Dtos
{
    public class AppSettings
    {
        public List<RobotConfiguration> Robots { get; set; } = new List<RobotConfiguration>();
        public List<UserMapping> DefaultUserMappings { get; set; } = new List<UserMapping>();
    }
    public class RobotConfiguration
    {
        public string Name { get; set; }
        public string Access_Token { get; set; }
        public string Secret { get; set; }
        public bool AtOnly { get; set; } = false;
        public List<WorkType> Types { get; set; } = new List<WorkType>();
        public List<UserMapping> UserMappings { get; set; } = new List<UserMapping>();
    }
    public class WorkType
    {
        public string Type { get; set; }
        public List<string> WorkItemTypes { get; set; } = new List<string>();
    }
}
