namespace Wallet.Domain.Entities;

public class Outbox : BaseEntity
{
    public string Content { get; set; }
    public DateTime? OccuredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Error { get; set; }
    public string Type { get; set; }
    public DateTime? SucceedOn { get; set; }
}
