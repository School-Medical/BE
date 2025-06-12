using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class UserRegisterProfile : Profile
    {
        public UserRegisterProfile() 
        {
            CreateMap<UserRegisterDTORequest, User>()
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.account, opt => opt.MapFrom(src => src.Account))
                .ForMember(dest => dest.hash_password, opt => opt.MapFrom(src => src.HashPassword))
                .ForMember(dest => dest.role_id, opt => opt.MapFrom(src => src.RoleId));
            CreateMap<User, UserRegisterDTOResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.account))
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.hash_password))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.role_id));

        }
    }
}
