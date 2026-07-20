using Consumer.Infrastracture;
using MassTransit;
using Producer.Models;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Consumer.Consumers;

public class TransactionConsumer(
    ConsumerDbContext dbContext,
    ILogger<TransactionConsumer> logger,
    IPublishEndpoint publishEndpoint) : IConsumer<InitiateTransactionCommand>
{
    public async Task Consume(ConsumeContext<InitiateTransactionCommand> context)
    {
        var transactionCommand = context.Message;

        logger.LogInformation("Received transaction, Id: {TransactionId}", transactionCommand.TransactionId);

        var transaction = new Transaction(transactionCommand.FromUserId, transactionCommand.ToUserId);

        dbContext.Transactions.Add(transaction);

        await dbContext.SaveChangesAsync(context.CancellationToken);

        await publishEndpoint.Publish(new TransactionProcessedEvent(transaction.Id, DateTime.UtcNow));

        logger.LogInformation("Transaction processed, Id: {TransactionId}", transactionCommand.TransactionId);
    }
}
