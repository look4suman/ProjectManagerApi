using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Entities
{
    [ExcludeFromCodeCoverage]
    public class UserModel
    {
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
    }
}