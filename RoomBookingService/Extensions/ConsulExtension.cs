//using Consul;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting.Server.Features;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RoomBookingService.Extensions
//{
//    public static class ConsulExtension
//    {
//        public static void RegisterConsulService(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config => {
//                var address = configuration["Consul:Host"];
//                config.Address = new System.Uri(address);
//            }));
//        }

//        public static void UseConsul(this IApplicationBuilder app)
//        {
//            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

//            //var features = app.Properties["server.Features"] as FeatureCollection;
//            //var addresses = features.Get<IServerAddressesFeature>();
//            //var address = addresses.Addresses.First();

//            //Console.WriteLine($"address={address}");

//            //var uri = new Uri(address);
//            var registration = new AgentServiceRegistration()
//            {
//                ID ="bookingID",
//                // servie name  
//                Name = "booking",
//                Address = "http://localhost",
//                Port = 2003
//            };

//            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
//            consulClient.Agent.ServiceRegister(registration).Wait();
//        }
//    }
//}
