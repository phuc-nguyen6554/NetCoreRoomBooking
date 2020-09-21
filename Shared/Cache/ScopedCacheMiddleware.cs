using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Cache
{
    class ScopedCacheMiddleware : IMiddleware
    {
        private IScopedCache _cache;
        public ScopedCacheMiddleware(IScopedCache cache)
        {
            _cache = cache;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string username = null;
            if(!String.IsNullOrEmpty(context.Request.Headers["X-Forwarded-Username"]))
            {
                username = Uri.UnescapeDataString(context.Request.Headers["X-Forwarded-Username"]);
            }
            _cache.Username = username;
            _cache.Email = context.Request.Headers["X-Forwarded-Email"];
            _cache.Avatar = context.Request.Headers["X-Forwarded-Avatar"];

            await next(context);
        }
    }

    public static class ScopedCacheMiddlewareConfigure
    {
        public static void AddScopedCacheService(this IServiceCollection services)
        {
            services.AddScoped<IScopedCache, ScopedCache>();
            services.AddScoped<ScopedCacheMiddleware>();
        }

        public static void UseScopedCacheMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ScopedCacheMiddleware>();
        }
    }
}
