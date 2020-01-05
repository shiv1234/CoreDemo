using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreDemo.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandeler(int statusCode)
        {
            var exceptionDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.Errormsg = "Request could not found.";
                    logger.LogWarning($" Error 404 Occured: Path {exceptionDetails.OriginalPath} and QS is {exceptionDetails.OriginalQueryString}.");
                    break;
                        
            }               

            return View("NotFound");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The Path {exceptionDetails.Path} throw an exception {exceptionDetails.Error}");
            return View();
        }
    }
}
