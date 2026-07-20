using Consumer.Consumers;
using MassTransit;

namespace Consumer.Extentions;

public static class MassTransitExt
{
    public static IServiceCollection RegisterMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitmq = configuration.GetSection("RabbitMq");

        services.AddMassTransit(busConf =>
        {
            busConf.AddConsumer<TransactionConsumer>();

            busConf.UsingRabbitMq((context, rabbitConf) =>
            {
                rabbitConf.Host(rabbitmq["Host"], hostConf =>
                {
                    hostConf.Username(rabbitmq["Username"]!);
                    hostConf.Password(rabbitmq["Password"]!);
                });

                rabbitConf.UseMessageRetry(conf => conf.Interval(3, 3000));
                rabbitConf.ConfigureEndpoints(context);

                rabbitConf.ReceiveEndpoint("transactions", conf =>
                {
                    conf.ConfigureConsumer<TransactionConsumer>(context);
                });
            });
        });

        return services;
    }
}
