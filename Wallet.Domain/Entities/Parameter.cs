namespace Wallet.Domain.Entities;

public class Parameter : BaseEntity
{
    public string ParamType { get; set; }
    public string ParamValue { get; set; }
    public string ParamDescription { get; set; }
}
