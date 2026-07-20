using MassTransit;
using Shared.Contracts.Commands;

namespace Consumer.Consumers;

public class TransactionConsumer(ILogger<TransactionConsumer> logger) : IConsumer<InitiateTransactionCommand>
{
    public async Task Consume(ConsumeContext<InitiateTransactionCommand> context)
    {
        var transaction = context.Message;

        logger.LogInformation("Received transaction, Id: {TransactionId}", transaction.TransactionId);
    }
}
