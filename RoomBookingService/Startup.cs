using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RoomBookingService.Models;
using Shared.Serilog;
using AutoMapper;
using RoomBookingService.Extensions;
using Shared.Exceptions;
using Shared.Cache;
using Shared.Consul;


namespace RoomBookingService
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
            services.AddDbContext<BookingServiceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BookingServiceContext"));
            });
            services.AddSerilogMiddleware();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Booking API",
                    Description = "Room Booking API"
                });
            });

            services.ConfigureService();
            services.RegisterServiceException();
            services.AddScopedCacheService();

            services.RegisterConsul();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.ConfigureServiceException();
            app.UseSerilogMiddleware();
            app.UseScopedCacheMiddleware();

            app.UseConsul("SS_Portal_Booking_ID", "SS_Portal_Booking", true);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API V1");
            });           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
