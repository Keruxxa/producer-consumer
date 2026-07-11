using Producer.Models.Enums;

namespace Producer.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public ProgressStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
