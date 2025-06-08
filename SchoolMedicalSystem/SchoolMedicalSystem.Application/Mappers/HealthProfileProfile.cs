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
    public class HealthProfileProfile : Profile
    {
        public HealthProfileProfile()
        {
            CreateMap<HealthProfile, HealthProfileDTOResponse>()
                .ForMember(dest => dest.student_name, opt => opt.MapFrom(src => (src.student.first_name + " " + src.student.last_name)));

            CreateMap<HealthProfileDTORequest, HealthProfile>();
        }
    }
}
