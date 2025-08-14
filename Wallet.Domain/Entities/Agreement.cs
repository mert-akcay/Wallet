namespace Wallet.Domain.Entities;

public class Agreement : BaseEntity
{
    public string Content { get; set; }

    public string Version { get; set; }

    public string Title { get; set; }
    public bool IsActive { get; set; }
    public int Type { get; set; }
}
