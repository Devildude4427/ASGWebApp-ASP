using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
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