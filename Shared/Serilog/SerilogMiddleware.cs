using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Context;

namespace Shared.Serilog
{
    public class SerilogMiddleware : IMiddleware
    {
        public SerilogMiddleware()
        {
            //Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File(@"D:\NetCore\Gateway.API\log.txt").CreateLogger();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            var requestID = context.Request.Headers["request-correlation-id"].ToString();
            LogContext.PushProperty("RCID", requestID);
            await next.Invoke(context);
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSerilogMiddleware(this IServiceCollection services)
        {
            services.AddScoped<SerilogMiddleware>();
            return services;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SerilogMiddleware>();
            return builder;
        }
    }
}
