using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTodo.Extensions
{
    public class UnAuthorizedAttribute : AuthorizeAttribute
    {
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new RedirectResult("/Error/AccessDenied");
        //    }
        //    else
        //    {
        //        base.HandleUnauthorizedRequest(filterContext);
        //    }
        //}
    }
}