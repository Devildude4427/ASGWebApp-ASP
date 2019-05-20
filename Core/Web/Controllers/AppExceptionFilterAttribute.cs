using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Web.Controllers
{
    public class AppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
//            if (context.Exception is SomeException)
//            {
//                do something
//            }
        }
    }
}