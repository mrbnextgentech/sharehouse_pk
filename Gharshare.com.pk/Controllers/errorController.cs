using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Gharshare.com.pk.Controllers
{
    public class ErrorController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public ErrorController(IUnitofWork unitofWork, Utility utility,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _env = env;
        }
        public IActionResult lockedout()
        {
            return View();
        }
        //[Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {                 
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    ViewBag.Path = TempData["StackTrace"];
                    ViewBag.QS = TempData["Source"];
                    break;
            }
            var errorViewModel = LogError();
            //return View(errorViewModel);
            return View("NotFound");
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var errorViewModel = LogError();
            return View(errorViewModel);
        }
        private ErrorViewModel LogError()
        {
            var errorMessage = TempData["ErrorMessage"]?.ToString();
            var errorSource = TempData["Source"]?.ToString();
            var stackTrace = TempData["StackTrace"]?.ToString();             
            var errorViewModel = new ErrorViewModel
                {
                    ErrorMessage = errorMessage,
                    ErrorSource = errorSource,
                    StackTrace = stackTrace,
                    ShowRequestId = false,                    
                    RequestId = HttpContext.TraceIdentifier                    
                };
            try
            {
                var errorPath = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Path ?? Request.Path.Value;
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                var errorLog = new ErrorLog
                {
                    ErrorDetail = errorViewModel.ErrorMessage,
                    ErrorSource = errorViewModel.ErrorSource,
                    StackTrace = errorViewModel.StackTrace,
                    ErrorPath = errorPath,
                    LoggedInUserId = HttpContext.Session.GetString("_UserId"),
                    status = ErrorLog.Status.Open,
                    Version = version,
                    EnvName = _env.EnvironmentName,
                    UId = Guid.NewGuid().ToString()
                    //HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error.StackTrace ?? TempData["StackTrace"].ToString()
                };
                _unitofWork.ErrorLog.Add(errorLog);
                _unitofWork.Save();
            }
            catch(Exception ex)
            {

            }
            return errorViewModel;
        }       
        public IActionResult Index()
        {
            return View();
        }
    }
}
