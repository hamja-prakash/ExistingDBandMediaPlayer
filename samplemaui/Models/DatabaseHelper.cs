namespace samplemaui.Models
{
    public class DatabaseHelper
    {
        public static string InsertEmployee = "Insert into employees(FirstName,LastName)Values(?,?)";
        public static string UpdateEmployee = "Update employees Set FirstName=?, LastName=? Where EmployeeId=?";
        public static string DeleteEmployee = "Delete from employees where EmployeeId=?";
        public static string AllEmployee = "Select * from employees";
    }
}
