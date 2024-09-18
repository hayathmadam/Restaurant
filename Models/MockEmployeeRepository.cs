
namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;


        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
        {
                // hard code of data
         new Employee() { Id = 1, Name = "haitahm", 
             Department = Dept.IT, Email = "hayathm1994@gmail.com" },
            new Employee() { Id = 2, Name = "Adam",
             Department = Dept.HR, Email = "hayathm26@yahoo.com" }
        };
    }

        public Employee add(Employee employee)
        {
            employee.Id = -_employeeList.Max(e => e.Id) +1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee delete(int id)
        {
          Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
           return  _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee update(Employee employeeChange)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChange.Id);
            if (employee != null)
            {
              employee.Name= employeeChange.Name;
              employee.Department= employeeChange.Department;
              employee.Email= employeeChange.Email;
            }
            return employee;
        }
    }
}


  
