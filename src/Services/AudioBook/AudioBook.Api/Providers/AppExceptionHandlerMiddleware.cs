using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AudioBook.Api.Providers
{
    public class AppExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AppExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                // customize as you need
                message = exception.Message,
                exception = exception.GetType(),
                stack_trace = exception.StackTrace
            }));
        }
    }

    public static class AppExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppExceptionHandlerMiddleware>();
        }
    }
}
