using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Consul
{
    public static class ConsulExtension
    {
        public static void RegisterConsul(this IServiceCollection services)
        {
            string ConsulHost = "http://localhost:8500";
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
            {
                config.Address = new Uri(ConsulHost);
            }));
        }

        public static void UseConsul(this IApplicationBuilder builder, string id, string name, bool heathcheck = false)
        {
            var consulClient = builder.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifetime = builder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var features = builder.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();

            var loggingFactory = builder.ApplicationServices
                               .GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            var uri = new Uri(address);
            var registration = new AgentServiceRegistration()
            {
                ID = id,
                // servie name  
                Name = name,
                Address = uri.Host,
                Port = uri.Port
            };
            
            if (heathcheck)
            {
                registration.Checks = new AgentServiceCheck[]
                {
                    new AgentServiceCheck
                    {
                        HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/heathcheck",
                        Timeout = TimeSpan.FromSeconds(5) ,
                        Interval = TimeSpan.FromSeconds(20),
                        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)                       
                    }
                };
            }

            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            logger.LogInformation("Registered " + registration.ID);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering " + registration.ID);
                consulClient.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();
            });
        }
    }
}
