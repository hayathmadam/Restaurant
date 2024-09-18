using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeManagment.controllers
{
    public class AccountController : Controller
    {
        
        private readonly SignInManager<Appuser> SignInManager;
        private readonly UserManager<Appuser> userManager;

        public AccountController(SignInManager<Appuser> signInManager, UserManager<Appuser> userManager)
        {
            // this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]

        public async  Task <IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
           return RedirectToAction("index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [AcceptVerbs("Get" ,"POST")]
        public async Task<IActionResult> InEmailUse(string Email)
        {

            var UserEmail= await userManager.FindByEmailAsync(Email);
            if(UserEmail == null)
            {

                return Json(true);

            }
            else
            {

                return Json($"Email: {Email} the already Use");

            }

        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Email!=null)
            {
                Appuser user = new()
                {
                    UserName=model.Email,
                    Email = model.Email,
                     EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    if (SignInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                    }
   
                 await SignInManager.SignInAsync(user, isPersistent:false);
                    return Json("response");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Json("response");
        }

        [AllowAnonymous]
        [HttpGet]

        public IActionResult login(string? returnUrl)
        {
            //LoginViewModel model = new LoginViewModel
            //{
            //   ReturnUrl = returnUrl,
            //   ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            //};

          return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password,false,false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {

                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                if (!result.Succeeded)
                    ModelState.AddModelError(string.Empty, "InValiad login attempt");

                return View(model);
            }


            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternaLogin(string provider , string returnUrl)
        {
           
            var redirctUrl = Url.Action("ExternalLoginCallBack","Account", new {ReturnUrl=returnUrl});
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirctUrl);

            return new ChallengeResult(provider, properties);   


        }




    }

}
