using System.Net;
using Newtonsoft.Json;

#nullable disable

namespace API.Configuration
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Method.Equals("options", StringComparison.CurrentCultureIgnoreCase) && httpContext.User == null)
                httpContext.User = System.Security.Principal.WindowsPrincipal.Current;

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                context.Response.StatusCode,
                exception.Message,
                exception.StackTrace
            }));
        }
    }
}