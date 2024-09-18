using EmployeeManagment.Uitilies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.ViewModels
{
    public class RegisterViewModel
    {




      

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [VaildEmailDomain(AllowDomain:"gmail.com", ErrorMessage = "Email Domain Must be gmail.com")]
        [Remote(action:"InEmailUse",controller:"Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("ConfirmPassword")]
        [Compare("Password",ErrorMessage ="the confirm password do not match ")]
        public string? ConfirmPassword { get; set;}

    }

}
