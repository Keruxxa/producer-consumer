namespace Shared.Contracts.Events;

public record TransactionProcessedEvent(Guid TransactionId, DateTime ProcessedAt);
