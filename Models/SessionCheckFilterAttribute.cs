using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Gharshare.Models
{
    public class SessionCheckFilterAttribute : ActionFilterAttribute
    {
        private readonly string[] excludedControllers = { "Home" };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.RouteData.Values["controller"]?.ToString();
            var session = context.HttpContext.Session;
            var sessionId = session.Id;

            if (string.IsNullOrEmpty(sessionId) && !(excludedControllers.Contains(controllerName)))
            {
                var loginPath = "/account/login";
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "identity", page = loginPath }));
                context.HttpContext.Items["Error"] = "Session timed out. Please log in again";
            }
            base.OnActionExecuting(context);
        }
    }
}
