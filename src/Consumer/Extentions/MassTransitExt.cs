using Consumer.Consumers;
using Consumer.Infrastracture;
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

                rabbitConf.ReceiveEndpoint("transactions", conf =>
                {
                    conf.ConfigureConsumer<TransactionConsumer>(context);
                    conf.UseEntityFrameworkOutbox<ConsumerDbContext>(context);
                });
            });

            busConf.AddEntityFrameworkOutbox<ConsumerDbContext>(conf =>
            {
                conf.UsePostgres();
                conf.UseBusOutbox();
            });
        });

        return services;
    }
}
