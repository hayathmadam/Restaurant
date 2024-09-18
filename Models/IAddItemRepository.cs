
namespace EmployeeManagment.Models
{
    public interface IAddItemRepository
    {
        IEnumerable<AddItem> GetAll();
        AddItem GetItem(int? id);
        AddItem add(AddItem addItem);
        AddItem update(AddItem addItemChange);
        AddItem delete(int id);
    }

}
