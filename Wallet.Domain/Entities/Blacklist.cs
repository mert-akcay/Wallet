namespace Wallet.Domain.Entities;

public class Blacklist : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Channel { get; set; }
    public string Reason { get; set; }
}
