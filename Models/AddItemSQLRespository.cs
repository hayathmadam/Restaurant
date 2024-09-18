
namespace EmployeeManagment.Models
{
    public class AddItemSQLRespository : IAddItemRepository
    {
        private readonly ApDbcontext context;
        public AddItemSQLRespository(ApDbcontext context)
        {
            this.context = context;
        }


        public AddItem add(AddItem addItem)
        {
            context.AddItems.Add(addItem);
            context.SaveChanges();
            return addItem;
        }

        public AddItem delete(int id)
        {
            AddItem addItem = context.AddItems.Find(id);
            if (addItem != null)
            {
                context.AddItems.Remove(addItem);
                context.SaveChanges();
            }
            return addItem;
        }

        public AddItem GetItem(int? id)
        {
            return context.AddItems.Find(id);
        }

        public IEnumerable<AddItem> GetAll()
        {
            return context.AddItems;
           
        }

        public AddItem update(AddItem addItemChange)
        {
            var additem = context.AddItems.Attach(addItemChange);
            additem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return addItemChange;
        }

    
    }
}
