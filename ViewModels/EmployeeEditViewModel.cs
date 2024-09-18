
namespace EmployeeManagment.ViewModels
{
    public class EmployeeEditViewModel:EmployeeCreateViewModel
    {

        public int Id { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
