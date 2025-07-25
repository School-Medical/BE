using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTOResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.account))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.role!.role_name))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.role!.role_name == "parent" ? src.StudentParents.Select(sp => sp.student) : src.Students));


            CreateMap<UserDTORequest, User>()
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }

}
