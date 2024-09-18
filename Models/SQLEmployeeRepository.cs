
namespace EmployeeManagment.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {

         private readonly ApDbcontext context;
        public SQLEmployeeRepository(ApDbcontext context) { 
        
        this.context = context;
        }
        public Employee add(Employee employee)
        {
           context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee delete(int id)
        {
            Employee employee =context.Employees.Find(id);
            if (employee != null)
            { 
            context.Employees.Remove(employee);
             context.SaveChanges() ;
            }
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee update(Employee employeeChange)
        {
            var employee = context.Employees.Attach(employeeChange);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChange;
        }
    }
}
