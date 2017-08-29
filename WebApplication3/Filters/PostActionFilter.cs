using System.Web.Mvc;
using BLL.LogFolder;
using Ninject;

namespace WebApplication3.Filters
{
    public class PostActionFilter : ActionFilterAttribute
    {
        [Inject]
        public ILogService Service { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
                Service.Log("Info", null, "Method {0} is executed", filterContext.ActionDescriptor.ActionName);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                Service.Log("Info", null, "Method {0} is executing", filterContext.ActionDescriptor.ActionName);
            }
        }
    }
}