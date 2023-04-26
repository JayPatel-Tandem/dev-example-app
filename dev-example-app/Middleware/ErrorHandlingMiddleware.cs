using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace dev_example_app.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public static async Task WriteErrorResponse(HttpContext httpContext, object errorResponse, HttpStatusCode statusCode)
        {
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";
            
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (ValidationException ex)
            {
                
                var failures = ex.Errors.Select(error => new
                {
                    message = error.ErrorMessage,
                    property = error.PropertyName
                });

                var errorResponse = new {Message = "Validation error occurred.", Error = failures};
                await WriteErrorResponse(context, errorResponse, HttpStatusCode.InternalServerError);
            }
        }
    }
}
