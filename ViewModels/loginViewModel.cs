using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EmployeeManagment.ViewModels
{
    public class LoginViewModel
    {


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [Display(Name ="RememberMe")]
        public bool RememberMe { get; set; }

        //public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }


    }
}
