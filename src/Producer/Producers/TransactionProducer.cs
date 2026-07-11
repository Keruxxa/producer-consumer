using MassTransit;
using Shared.Contracts.Commands;

namespace Producer.Producers;

public class TransactionProducer(IPublishEndpoint publishEndpoint, ILogger<TransactionProducer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(Random.Shared.Next(500, 3000), stoppingToken);

            var transaction = new InitiateTransactionCommand
            {
                TransactionId = Guid.CreateVersion7(),
                FromUserId = Guid.CreateVersion7(),
                ToUserId = Guid.CreateVersion7(),
                CreatedAt = DateTime.UtcNow
            };

            await publishEndpoint.Publish(transaction, stoppingToken);

            logger.LogInformation("Transaction {TransactionId} was published", transaction.TransactionId);
        }
    }
}
