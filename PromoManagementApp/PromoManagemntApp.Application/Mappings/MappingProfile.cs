using AutoMapper;
using PromoManagemntApp.Application.DTOs.Register;
using PromoManagemntApp.Domain.Entities;

namespace PromoManagemntApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
