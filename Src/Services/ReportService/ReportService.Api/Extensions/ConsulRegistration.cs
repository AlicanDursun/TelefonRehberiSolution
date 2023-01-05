using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;

namespace ReportService.Api.Extensions
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration["ConsulConfig:Address"]!;
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            //Get server IP address
            lifetime.ApplicationStarted.Register(() =>
            {
                var uri = configuration.GetValue<Uri>("ConsulConfig:ServiceAddress");
                var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
                var serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId");

                //Register service with consul

                var registration = new AgentServiceRegistration()
                {
                    ID = serviceId ?? "ReportService",
                    Name = serviceName ?? "ReportService",
                    Address = $"{uri.Host}",
                    Port = uri.Port,
                    Tags = new[] { serviceName, serviceId }
                };

                logger.LogInformation("Registering with Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(async () =>
                {
                    logger.LogInformation("Deregistering from Consul");
                    await consulClient.Agent.ServiceDeregister(registration.ID);
                });


            });
            return app;
        }
    }
}
