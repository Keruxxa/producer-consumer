using Producer.Models.Enums;

namespace Producer.Models;

public class Transaction(Guid fromUserId, Guid toUserId)
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid FromUserId { get; set; } = fromUserId;
    public Guid ToUserId { get; set; } = toUserId;
    public ProgressStatus Status { get; set; } = ProgressStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
