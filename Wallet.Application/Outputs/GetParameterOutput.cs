namespace Wallet.Application.Outputs;

public class GetParameterOutput
{
    public List<GetParameterModel> Parameters { get; set; }
}

public class GetParameterModel
{
    public string? ParamType { get; set; }
    public string? ParamValue { get; set; }
    public string? ParamDescription { get; set; }
}