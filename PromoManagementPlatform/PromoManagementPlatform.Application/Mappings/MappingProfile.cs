using AutoMapper;
using PromoManagementPlatform.Application.DTOs.Campaign;
using PromoManagementPlatform.Application.DTOs.Register;
using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<Domain.Entities.Campaign, CampaignDTO>();
        }
    }
}
