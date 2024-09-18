using EmployeeManagment.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.ViewModels
{
    public class EmployeeCreateViewModel
    {
       
        [Required]
        [MaxLength(5, ErrorMessage = "The Length Not Invlid")]
        public string Name { get; set; }
        [Required]
        public Dept? Department { get; set; }
        [Required]
        //[RegularExpression("#",ErrorMessage ="not Invlid")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
       public IFormFile? Photo { get; set; }
    }
}
