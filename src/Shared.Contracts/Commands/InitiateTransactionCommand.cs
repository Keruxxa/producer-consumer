namespace Shared.Contracts.Commands;

public class InitiateTransactionCommand
{
    public Guid TransactionId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
