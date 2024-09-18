using EmployeeManagment.Models;

namespace EmployeeManagment.ViewModels
{
    public class ItemViewItemCreateViewModel:ItemCreateViewModel
    {
        public IEnumerable<ItemType> ItemType { get; set; }
    }
}
