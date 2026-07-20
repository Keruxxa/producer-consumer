using MassTransit;
using Shared.Contracts.Commands;

namespace Producer.Extensions;

public static class MassTransitExt
{
    public static IServiceCollection RegisterMassTransit(this IServiceCollection services, ConfigurationManager configuration)
    {
        var rabbitmq = configuration.GetSection("RabbitMq");

        services.AddMassTransit(busConf =>
        {
            busConf.UsingRabbitMq((context, rabbitConf) =>
            {
                rabbitConf.Host(rabbitmq["Host"], hostConf =>
                {
                    hostConf.Username(rabbitmq["Username"]!);
                    hostConf.Password(rabbitmq["Password"]!);
                });

                rabbitConf.UseMessageRetry(conf => conf.Interval(3, 3000));
                rabbitConf.ConfigureEndpoints(context);
            });

            MapMessageTypes();
        });

        return services;
    }

    private static void MapMessageTypes()
    {
        EndpointConvention.Map<InitiateTransactionCommand>(new Uri($"queue:transactions"));
    }
}
