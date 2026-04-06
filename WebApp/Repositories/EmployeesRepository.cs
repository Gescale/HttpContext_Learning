namespace WebApp.Repositories
{
    static public class EmployeesRepository
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee(1, "John Doe", "Software Engineer", 60000),
            new Employee(2, "Jane Smith", "Project Manager", 75000),
            new Employee(3, "Emily Johnson", "UX Designer", 55000),
            new Employee(4, "Michael Brown", "Data Analyst", 65000),
            new Employee(5, "Sarah Davis", "QA Engineer", 50000),
            new Employee(6, "David Wilson", "DevOps Engineer", 70000)
        };
        
        public static List<Employee> GetAllEmployees() => employees;

        public static void AddEmployee(Employee employee)
        {
            if (employee != null)
                employees.Add(employee);

            else
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }

        public static Employee? GetEmployeeById(string id)
        {
            if (int.TryParse(id, out int employeeId))
            {
                return employees.FirstOrDefault(e => e.Id == employeeId);
            }
            else
            {
                throw new ArgumentException("Invalid employee ID format. ID must be an integer.", nameof(id));
            }
        }
    }
}
