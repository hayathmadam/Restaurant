

using EmployeeManagment.Models;
namespace EmployeeManagment.ViewModels
{
    public class ItemCreateViewModel
    {
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemType { get; set; }
    }
}
