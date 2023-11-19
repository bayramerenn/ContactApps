using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.BaseModels;

namespace NewInn.Shared.Filters
{
    public class DefaultResponseAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var apiResponse = new ApiResponse<object>
                {
                    Data = objectResult.Value,
                    StatusCode = objectResult.StatusCode
                };

                context.Result = new ObjectResult(apiResponse);
            }

            base.OnResultExecuting(context);
        }
    }
}