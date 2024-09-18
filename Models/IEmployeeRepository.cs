
namespace EmployeeManagment.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);

        IEnumerable<Employee> GetAll();
        Employee  add(Employee employee);
        Employee update(Employee employeeChange);
        Employee delete(int id);
    }
}
