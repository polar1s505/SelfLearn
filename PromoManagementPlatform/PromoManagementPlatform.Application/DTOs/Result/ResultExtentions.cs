using Microsoft.AspNetCore.Mvc;

namespace PromoManagementPlatform.Application.DTOs.Result
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<T>(this Result<T> result, string successMessage)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(successMessage);
            }

            return new BadRequestObjectResult(result.Error);
        }
    }
}
