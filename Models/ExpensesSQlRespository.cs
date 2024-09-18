
using NuGet.Configuration;

namespace EmployeeManagment.Models
{
    public class ExpensesSQlRespository : IExpensesRespository
    {
        private readonly ApDbcontext context;
        public  ExpensesSQlRespository(ApDbcontext context)
        {
            this.context = context;
        }
        public Expenses add(Expenses expenses)
        {
            context.Expenses.Add(expenses);
            context.SaveChanges();
            return expenses;
        }

        public Expenses delete(int id)
        {
            Expenses expenses = context.Expenses.Find(id);
            if (expenses != null)
            {
                context.Expenses.Remove(expenses);
                context.SaveChanges();
            }
            return expenses;
        }

        public IEnumerable<Expenses> GetExpenses()
        {
            return context.Expenses;
        }

        public Expenses GetItem(int? id)
        {
            return context.Expenses.Find(id);
        }

        public Expenses update(Expenses expenses)
        {
            var addexpenses = context.Expenses.Attach(expenses);
            addexpenses.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return expenses;
        }
    }
}
