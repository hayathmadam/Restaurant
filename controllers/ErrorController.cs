using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace MYfirstproject.Controllers
{
    public class ErrorController : Controller
    {

        //private readonly ILogger <ErrorController> logger;
        //public ErrorController(ILogger <ErrorController> logger) 
        
        //{ 
        //this.logger = logger;
        
        
        //}

        [Route("Error/{Statuscode}")]
        public IActionResult HttpStatusCodeHandler(int Statuscode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();  
            switch(Statuscode)
                {
                case 404:
                    ViewBag.ErrorMessage = "Sorry,The resource you requested could not be Found ";
                   // logger.LogInformation(statusCodeResult.OriginalPath);
                 
                    break;
            }
            return View("NotFound");
        }


        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
         
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.EXceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
          // logger.LogError("the path "+exceptionDetails.Path);
            return View("Error");
        }
    }
}
