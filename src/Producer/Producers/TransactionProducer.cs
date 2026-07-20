using MassTransit;
using Shared.Contracts.Commands;

namespace Producer.Producers;

public class TransactionProducer(IServiceProvider serviceProvider, ILogger<TransactionProducer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = serviceProvider.CreateScope();
            var sendEndpointProvider = scope.ServiceProvider.GetRequiredService<ISendEndpointProvider>();

            await Task.Delay(Random.Shared.Next(500, 3000), stoppingToken);

            var transaction = new InitiateTransactionCommand
            {
                TransactionId = Guid.CreateVersion7(),
                FromUserId = Guid.CreateVersion7(),
                ToUserId = Guid.CreateVersion7(),
                CreatedAt = DateTime.UtcNow
            };

            await sendEndpointProvider.Send(transaction, stoppingToken);

            logger.LogInformation("Transaction published, Id: {TransactionId} ", transaction.TransactionId);
        }
    }
}
