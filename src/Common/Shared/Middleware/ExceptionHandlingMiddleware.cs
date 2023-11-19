using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.BaseModels;
using Shared.Exceptions;
using System.Net;
using ValidationException = Shared.Exceptions.ValidationException;

namespace Shared.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogError(ex, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string message = exception.Message;

            if (exception is ValidationException validation)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var apiResponse = new ApiResponse<object>
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message,
                    Errors = validation.Errors
                };

                await context.Response.WriteAsync(apiResponse.ToString());
            }
            else
            {
                if (exception is DuplicateException)
                {
                    code = HttpStatusCode.Conflict;
                }
                else if (exception is ArgumentException)
                {
                    code = HttpStatusCode.NotFound;
                }
                else if (exception is NotFoundException)
                {
                    code = HttpStatusCode.NotFound;
                }
                else if (exception is BadRequestException)
                {
                    code = HttpStatusCode.BadRequest;
                }

                context.Response.StatusCode = (int)code;

                await context.Response.WriteAsync(new ApiResponse<object>
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message,
                }.ToString());
            }
        }
    }
}