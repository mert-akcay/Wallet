using AutoMapper;
using Wallet.Application.Outputs;

namespace Wallet.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Wallet, WalletByWalletIdOutput>()
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.Id));
    }
}
