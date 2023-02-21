using samplemaui.Models;
using SQLite;

namespace samplemaui.Helpers
{
    public class EmployeeRepository
    {
        private readonly SQLiteConnection _database;

        public static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "chinook.db");
       
        public EmployeeRepository()
        {
            _database = new SQLiteConnection(DbPath);
            _database.CreateTable<Employee>();
        }

        public List<Employee> List()
        {
           // return _database.Table<Employee>().ToList();
            return _database.Query<Employee>(DatabaseHelper.AllEmployee, new object[] { }).ToList();
        }

        public bool SaveEmployee(Employee employee)
        {
            return _database.Execute(DatabaseHelper.InsertEmployee, new object[] {employee.FirstName, employee.LastName,
                                                                           }) == 1 ? true : false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            return _database.Execute(DatabaseHelper.UpdateEmployee, new object[] {employee.FirstName, employee.LastName,
                                                                           employee.EmployeeId }) == 1 ? true : false;
        }

        public bool DeleteEmployee(int Id)
        {
            _database.Query<Employee>(DatabaseHelper.DeleteEmployee, Id);
            return true;
        }
    }
}
