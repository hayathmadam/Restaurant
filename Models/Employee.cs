using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Models
{
    public class Employee
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(5,ErrorMessage ="The Length Not Invlid")]
        public string? Name { get; set; }
        [Required]
        public  Dept? Department { get; set; }
        [Required]
        //[RegularExpression("#",ErrorMessage ="not Invlid")]
        [Display(Name="Office Email")]
        public   string? Email {  get; set; }
        public string? photopat {  get; set; }
 

    }
}
