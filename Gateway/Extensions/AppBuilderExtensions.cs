using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Exceptions;

namespace Gateway.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddleware(this IApplicationBuilder builder)
        {
            //builder.UseMiddleware<ServiceExceptionMiddleware>();
        }
    }
}
