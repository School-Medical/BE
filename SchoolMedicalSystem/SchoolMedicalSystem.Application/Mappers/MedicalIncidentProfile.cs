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
    public class MedicalIncidentProfile : Profile
    {
        public MedicalIncidentProfile() {
            CreateMap<MedicalIncident, MedicalIncidentDTOResponse>()
                .ForMember(dest => dest.nurse_name, opt => opt.MapFrom(src => (src.nurse.first_name + src.nurse.last_name)))
                .ForMember(dest => dest.student_name, opt => opt.MapFrom(src => (src.student.first_name + src.student.last_name)));
            
            CreateMap<MedicalIncidentDTORequest, MedicalIncident>();
            }
    }
}
