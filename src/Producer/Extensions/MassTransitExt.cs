using MassTransit;
using Shared.Contracts.Commands;

namespace Producer.Extensions;

public static class MassTransitExt
{
    public static IServiceCollection RegisterMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(busConf =>
        {
            busConf.UsingRabbitMq((context, rabbitConf) =>
            {
                rabbitConf.Host("rabbitmq", hostConf =>
                {
                    hostConf.Username("guest");
                    hostConf.Password("guest");
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
        EndpointConvention.Map<InitiateTransactionCommand>(new Uri($"queue:transaction-commands"));
    }
}
