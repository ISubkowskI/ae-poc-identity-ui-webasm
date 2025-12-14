using AutoMapper;
using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.UiData;

namespace Ae.Poc.Identity.Ui.Profiles;

public class UiDataProfile : Profile
{
    public UiDataProfile()
    {
        CreateMap<AppClaimDto, AppClaimUiItem>();
        CreateMap<AppClaimUiItem, AppClaimDto>();

        CreateMap<AppAccountDto, AppAccountUiItem>();
    }
}
