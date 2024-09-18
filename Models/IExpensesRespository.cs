namespace EmployeeManagment.Models
{
    public interface IExpensesRespository
    {

        IEnumerable<Expenses> GetExpenses();
        Expenses add(Expenses expenses);
        Expenses delete(int id);
        Expenses update(Expenses expenses);
        Expenses GetItem(int? id);
    }
}
