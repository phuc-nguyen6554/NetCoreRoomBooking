using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Serilog;
using Shared.Exceptions;
using Shared.Cache;
using Identity.Extensions;

namespace Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(option => option.UseSqlServer(Configuration.GetConnectionString("IdentityContext")));
            
            services.AddAutoMapper(typeof(Startup));


            services.ConfigureJWT(Configuration);
            services.ConfigureService();

            services.ConfigureSwagger();

            services.AddSerilogMiddleware();
            services.RegisterServiceException();
            services.AddScopedCacheService();

            services.AddControllers().AddNewtonsoftJson();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSerilogMiddleware();
            app.ConfigureServiceException();
            app.UseScopedCacheMiddleware();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
