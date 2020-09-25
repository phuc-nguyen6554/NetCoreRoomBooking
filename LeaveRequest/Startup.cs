using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using LeaveRequest.Models;
using Shared.Serilog;
using AutoMapper;
using LeaveRequest.Services.Implements;
using LeaveRequest.Services;
using Shared.Cache;
using Shared.Consul;

namespace LeaveRequest
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
            services.AddDbContext<LeaveRequestServiceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LeaveRequestServiceContext"));
            });
            
            services.AddSerilogMiddleware();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Leave Request API",
                    Description = "Leave Request API"
                });
            });

            services.AddScopedCacheService();
            services.AddScoped<ILeaveRequestService,LeaveRequestService>();
            services.RegisterConsul();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseScopedCacheMiddleware();

            app.UseSerilogMiddleware();
            app.UseConsul("SS_Portal_LeaveRequest_ID", "SS_Portal_LeaveRequest");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leave Request API V1");
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
