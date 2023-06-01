using Microsoft.AspNetCore.Mvc.Filters;

namespace RestoranManager.Controllers.ProductController
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

            }
            base.OnActionExecuting(context);
        }
    }
}
