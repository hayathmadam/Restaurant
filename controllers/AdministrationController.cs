using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Data.Common;
using System.Security.Claims;

namespace EmployeeManagment.controllers
{

      //[Authorize(Roles = "Admin")]
     //[Authorize(Policy = "AdminRolepolicy")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Appuser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<Appuser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager=userManager;

        }

        [AllowAnonymous]
        public   IActionResult AccessDenied()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserCalicms(string userid)
        {
            ViewBag.userid = userid;

            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"User with id = {userid}  cannot be found";
                return View("NotFound");

            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var Model = new UserClaimsViewModel
            {

                UserId = userid,
            };
            foreach (Claim claim in ClaimsStore.Allclaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    CliamType = claim.Type,

                };
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {

                    userClaim.IsSelected = true;
                }

                Model.Claims.Add(userClaim);
            }


            return View(Model);
        }



        [HttpPost]
        public async Task<IActionResult> ManageUserCalicms(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"user with id = {model.UserId} cannot be found";
                return View("NotFound");

            }


            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "cannot Remove user  existing  claims");
                return View(model);

            }

            result = await userManager.AddClaimsAsync(user, model.Claims
               .Select(c => new Claim(c.CliamType, c.IsSelected ? "true" : "false")));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add  seleted claims to user");
                return View(model);


            }
            return RedirectToAction("EditUser", new { id = model.UserId });

        }

        [HttpGet]
        public async Task<IActionResult> ManageUesrRoles (string userId)
        {
            ViewBag.userid = userId;
            var user = await userManager.FindByIdAsync(userId);
            if(user==null)
            {
                ViewBag.ErrorMessage = $"User with id ={userId} cannot be founed";
                return View("NotFound");


            }

            var model = new List<UserRolesViewModel>();
           foreach(var role in roleManager.Roles)
            {

                var UserRolesViewModel = new UserRolesViewModel
                {

                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if(await userManager.IsInRoleAsync(user,role.Name))
                {

                    UserRolesViewModel.IsSelected= true;

                }
                else
                {
                    UserRolesViewModel.IsSelected = false;

                }
                model.Add(UserRolesViewModel);

            }
           return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ManageUesrRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user==null)
            {

                ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
                return View("NotFound");

            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "cannot remove users existing Roles ");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if(!result.Succeeded)
            {

                ModelState.AddModelError("", "cannot add selected roles  to  user");
                return View(model); 

            }


            return RedirectToAction("EditUser", new { Id = userId });


        }



        [HttpGet]
        public IActionResult ListUsers()
        {
            return View();
        }
        public IActionResult GetUser()
        {

            var users = userManager.Users;

            return Json(users);


        }
            [HttpGet]
       

        [HttpPost]

        public async Task<IActionResult> DeleteUser(string id)
        {
         var user = await userManager.FindByIdAsync (id);
            if(user==null)
            {
                ViewBag.ErrorMessage = $"User With id ={id} cannot be found";
                return View("NotFound");
            }
            else
            {
           var result = await userManager.DeleteAsync(user);    
                if(result.Succeeded)
                {

                    return RedirectToAction("ListUsers");
                }
        else
                {

                    foreach (var Error in result.Errors)
                    { 
                     ModelState.AddModelError("", Error.Description);
                    
                    }

                    return View("ListUsers");
                }

         
            }

          

        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"User with Id = {id} cannot be Found";
                return View("NotFound");

            }


            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {

                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = (List<string>)userRoles,

            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult>EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync (model.Id);
            if(user==null)
            {
                ViewBag.ErrorMessage = $"User with id ={model.Id} cannot be found";

            }
            else
            {
             user.UserName= model.UserName;
              user.Email= model.Email;
               
                var result = await userManager.UpdateAsync(user);
                if(result.Succeeded)
                {

                    return Json("response");
                }
                else
                {

                    foreach(var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);

                    }    

                }

            }

            return View(model);

        }

        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName,
             
                };

         IdentityResult result = await roleManager.CreateAsync(identityRole);

                if(result.Succeeded)
                {
                 return RedirectToAction("ListRole", "Administration");
                }
               foreach(IdentityError error in result.Errors)
                {

                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
        [HttpGet]
        public IActionResult ListRole()
        {

            var Roles = roleManager.Roles;

            return View(Roles);
        }

        [HttpGet]
        [Authorize(Policy = "EditRolepolicy")]
        public async Task<IActionResult> EditRole( string Id)
        {
         var role = await  roleManager.FindByIdAsync(Id);
            if(role == null)
            {

                ViewBag.ErrorMessage = $"Role With Id={Id} cannot be found";
                return View("NotFound");

            }
            var model = new EditRoleViewModel
            {

                Id = role.Id,
                RoleName = role.Name

            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }

            }


            return View(model);


        }

        [HttpPost]
        [Authorize(Policy = "EditRolepolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role =await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"Role With id {model.Id} cannot be found";
                return View("NotFound");

            }
            else
            {
                role.Name = model.RoleName;
                var  result =await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("ListRole");
                }

                foreach(var Error in result.Errors) {
                    ModelState.AddModelError("", Error.Description);
                
                }

                return View(model);

            }

        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"User With id ={id} cannot be found";
                return View("NotFound");
            }


            else
            {
                try
                {

                   // throw new Exception("test");

                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("ListRole");
                    }
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);


                    }
                    return View("ListRole");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    @ViewBag.ErrorMessage = $"{role.Name}  role  cannot be Deleted as there are Users"
                        +$" in this role .if you want delete this role,Please Remove the Users form "
                        +$"the role and then try to Delete";
                    return View("Error");
                }
            }
        }
            

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string RoleId)
        {

             ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"The Role with id {RoleId} cannot be Founcd";

            }
            var model = new List<UserRoleViewModel>(); 
            foreach(var user in userManager.Users)
            {
                var userRoleviewModel = new UserRoleViewModel
                {

                 UserId = user.Id,
                 UserName = user.UserName

                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {

                    userRoleviewModel.IsSelected = true;
                }
                else
                {
                    userRoleviewModel.IsSelected = false;

                }
                model.Add(userRoleviewModel);   

            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model ,string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            if(role == null)
            {

                ViewBag.ErrorMessage = $"Role with the id {RoleId} cannot be Found";
                return View("NotFound");
            }

            for(int i=0; i<model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                   IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;

                }
                if(result.Succeeded)
                {

                    if (i < (model.Count - 1))
                    {

                        continue;
                    }
                    else
                    {

                        return RedirectToAction("EditRole",new { id = RoleId });
                    }
                    

                }

            }

            return RedirectToAction("EditRole", new { id = RoleId });
        }

        


    }
}
