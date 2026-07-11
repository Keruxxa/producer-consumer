namespace Shared.Contracts.Events;

public class TransactionProcessedEvent
{
    public Guid TransactionId { get; set; }
    public DateTime ProcessedAt { get; set; }
}
