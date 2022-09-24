using CarBuddy.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CarBuddy.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(exception);

            await context.Response.WriteAsync(new
            {
                context.Response.StatusCode,
                exception.Message,
            }.ToString());
        }

        private HttpStatusCode GetStatusCode(Exception exception)
        {
            if (exception.GetType() == typeof(EntityAlreadyExists))
                return HttpStatusCode.Forbidden;

            return HttpStatusCode.InternalServerError;
        }
    }
}
