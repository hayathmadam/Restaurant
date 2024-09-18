
namespace EmployeeManagment.Models
{
    public interface IitemtypeRepository
    {
        
        IEnumerable<ItemType> GetAll();
        ItemType add(ItemType itemtype);
        ItemType update(ItemType itemtypeChange);
        ItemType delete(int id);
        ItemType GettypeItem(int id);
    }
}
