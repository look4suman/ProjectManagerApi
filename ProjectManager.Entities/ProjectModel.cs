using System;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Entities
{
    [ExcludeFromCodeCoverage]
    public class ProjectModel
    {
        public int Project_ID { get; set; }
        public string Project { get; set; }
        public int Priority { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Boolean SetDate { get; set; }
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTasks { get; set; }
    }
}
