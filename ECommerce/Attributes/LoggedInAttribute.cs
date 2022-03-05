using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Attributes
{
    public class LoggedInAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserId") == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
