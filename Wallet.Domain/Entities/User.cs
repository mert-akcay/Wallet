namespace Wallet.Domain.Entities;

public class User : SoftDeletableEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string IdentityNumber { get; set; }
    public DateTime? BirthDate { get; set; }

    public string Email { get; set; }
    public bool IsEmailApproved { get; set; }

    public string PhoneNumber { get; set; }

    public bool IsActive { get; set; }
    public bool IsKyc { get; set; }
    public DateTime? KycDate { get; set; }

    public byte SimStatus { get; set; }

    public ICollection<UserAddress> Addresses { get; set; }
    public ICollection<Wallet> Wallets { get; set; }
    public ICollection<Blacklist> Blacklists { get; set; }
}
