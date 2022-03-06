
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Helper.Attributes
{
    public class LoggedInAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserId") == null)
            {
                filterContext.Result = new RedirectToActionResult("Login", "Authentication", "");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
