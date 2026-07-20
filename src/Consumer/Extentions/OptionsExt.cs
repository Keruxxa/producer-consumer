using Producer.Infrastructure.Options;

namespace Producer.Extensions;

public static class OptionsExt
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMq>(configuration.GetSection("RabbitMq"));

        return services;
    }
}
