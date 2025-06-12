using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
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
            CreateMap<User, UserRegisterDTORequest>();
        }
    }
}
