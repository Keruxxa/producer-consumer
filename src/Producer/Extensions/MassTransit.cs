using MassTransit;

namespace Producer.Extensions;

public static class MassTransit
{
    public static IServiceCollection RegisterMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.UsingRabbitMq((context, conf) =>
            {
                conf.Host("rabbitmq://localhost", conf =>
                {
                    conf.Username("admin");
                    conf.Password("admin");
                });

                conf.UseMessageRetry(conf => conf.Interval(3, 3000));
            });
        });

        return services;
    }
}
