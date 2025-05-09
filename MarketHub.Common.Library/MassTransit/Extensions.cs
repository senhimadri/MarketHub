using MarketHub.Common.Library.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MarketHub.Common.Library.MassTransit;

public static class Extensions
{

    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());

            config.UsingRabbitMq((context, configurator)=>
            {
                var configuration = context.GetService<IConfiguration>();

                var servicesSettings = configuration!.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var rabbitMqSettings = configuration!.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();

                configurator.Host(rabbitMqSettings!.Host, h =>
                {
                    h.Username(rabbitMqSettings.UserName);
                    h.Password(rabbitMqSettings.Password);
                });

                configurator.ConfigureEndpoints(context, 
                            new KebabCaseEndpointNameFormatter(prefix: servicesSettings!.ServiceName, 
                                                                includeNamespace: false));

                configurator.UseMessageRetry(retryConfig=>
                {
                    retryConfig.Interval(3,TimeSpan.FromSeconds(5));
                });

            });

        });

        return services;
    }
}
