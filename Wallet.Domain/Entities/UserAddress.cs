namespace Wallet.Domain.Entities;

public class UserAddress : SoftDeletableEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ProvinceId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid NeighborhoodId { get; set; }
}
