using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ServiceExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestId = context.Request.Headers["request-correlation-id"];
            LogContext.PushProperty("RCID", requestId);
            try
            {
                await next(context);
            }
            catch (ServiceException serviceEx)
            {
                await HandleExceptionAsync(context, serviceEx.response);
            }catch(Exception ex)
            {
                var error = new ErrorResponse(400, ex.Message);
                await HandleExceptionAsync(context, error);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.HttpStatus;
            var text = response.ToString();
            Log.Error(text);
            await context.Response.WriteAsync(text);
        }
    }

    public static class ServiceExceptionService
    {
        public static void RegisterServiceException(this IServiceCollection services)
        {
            services.AddScoped<ServiceExceptionMiddleware>();
        }

        public static void ConfigureServiceException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ServiceExceptionMiddleware>();
        }
    }
}
