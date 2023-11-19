using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels;

namespace ApiGateway.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ActionResult<T>(this ApiResponse<T> apiResponse)
        {
            return new ObjectResult(apiResponse)
            {
                StatusCode = (int)apiResponse.StatusCode
            };
        }
    }
}
