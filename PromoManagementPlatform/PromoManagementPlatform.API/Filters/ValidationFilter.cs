using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PromoManagementPlatform.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(context => context.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                var errorResponse = new
                {
                    IsValid = false,
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(errorResponse);
                return; 
            }

            await next();
        }
    }
}
