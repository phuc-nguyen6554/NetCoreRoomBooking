using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Cache
{
    public class ScopedCache : IScopedCache
    {
        public Guid RCID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();
    }

    //public static class CacheConfigure
    //{
    //    public static void ConfigureScopedCache(this IServiceCollection services)
    //    {
    //        services.AddScoped<IScopedCache, ScopedCache>();
    //    }
    //}
}
