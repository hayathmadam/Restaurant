using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagment.security
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler :AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {

            var authFilterContext = context.Resource as AuthorizationFilterContext;
                if(authFilterContext==null)
            {  
                return Task.CompletedTask; 
            }

                string LoggedInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string AdminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            if(context.User.IsInRole("Admin") && context.User.HasClaim(Claim => Claim.Type=="Edit Role" && Claim.Value=="true")
                && AdminIdBeingEdited.ToLower()  != LoggedInAdminId.ToLower())
            {

                context.Succeed(requirement);
            }
            return Task.CompletedTask;


        }

    }


}
