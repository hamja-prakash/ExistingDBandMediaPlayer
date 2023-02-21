using SQLite;

namespace samplemaui.Models
{
    [Table("employees")]
    public class Employee
    {
        [AutoIncrement, PrimaryKey]
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
