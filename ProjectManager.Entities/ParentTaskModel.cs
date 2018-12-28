using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Entities
{
    [ExcludeFromCodeCoverage]
    public class ParentTaskModel
    {
        public int Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }
}
