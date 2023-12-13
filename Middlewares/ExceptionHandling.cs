using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace proj_csharp_kiminoyume.Middlewares
{
    public class ExceptionHandling : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandling(ILogger<ExceptionHandling> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                LogError(exception);
                await HandleExceptionAsync(context, exception);
            }
        }

        private void LogError(Exception exception)
        {
            // Sample exception returned
            // FullName: "proj_csharp_kiminoyume.BusinessLogics.DreamCategoryBusinessLogic+<UpdateCategory>d__4"
            // Name: "<UpdateCategory>d__4"
            // Namespace: "proj_csharp_kiminoyume.BusinessLogics"

            string? stackTraceFullName = exception?.TargetSite?.DeclaringType?.Name;
            string? stackTrace = exception?.StackTrace;

            var exceptionDetails = $"{stackTraceFullName} | {exception?.Message} | {stackTrace}";
            Console.WriteLine(exceptionDetails);
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Status = (int)HttpStatusCode.InternalServerError;
            problemDetails.Title = "Server Error";

            if (exception is UnauthorizedAccessException)
            {
                problemDetails.Detail = "You do not have permission to access this resource.";
                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is ArgumentNullException)
            {
                problemDetails.Detail = "Input cannot be null.";
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is SqlException)
            {
                problemDetails.Detail = "Unable to process request. Please try again or contact the administrator";
            }
            else
            {
                problemDetails.Detail = "An unexpected error occurred while processing your request.";
            }

            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}