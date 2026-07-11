using Producer.Producers;

namespace Producer.Extensions;

public static class ProducersExt
{
    public static IServiceCollection RegisterProducers(this IServiceCollection services)
    {
        services.AddHostedService<TransactionProducer>();

        return services;
    }
}
