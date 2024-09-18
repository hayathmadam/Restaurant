
namespace EmployeeManagment.Models
{
    public class ItemTypeSQLRespository : IitemtypeRepository
    {
        private readonly ApDbcontext context;
        public ItemTypeSQLRespository(ApDbcontext context)
        {
            this.context = context;
        }
        public ItemType add(ItemType itemtype)
        {
            context.ItemTypes.Add(itemtype);
            context.SaveChanges();
            return itemtype;
        }

        public ItemType delete(int id)
        {
            ItemType itemType = context.ItemTypes.Find(id);
            if (itemType != null)
            {
                context.ItemTypes.Remove(itemType);
                context.SaveChanges();
            }
            return itemType;
        }
        public IEnumerable<ItemType> GetAll()
        {
            return context.ItemTypes;
        }

        public ItemType GettypeItem(int id)
        {
            return context.ItemTypes.Find(id);

        }

        public ItemType update(ItemType itemtypeChange)
        {
            var itemtype = context.ItemTypes.Attach(itemtypeChange);
            itemtype.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemtypeChange;
        }
    }
}
