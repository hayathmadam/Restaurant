using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EmployeeManagment.ViewModels
{
    public class EditUserViewModel
    {

        public EditUserViewModel() 
        {

        Roles = new List<string>();
        Claims = new List<string>();

        }

        public string? Id { get; set; }
        [Required]
        public string? UserName { get; set;}
        [Required]
        public string Email { get; set; }

        public List<string>Roles { get; set; }

        public List<string> Claims{ get; set; }

    }
}
