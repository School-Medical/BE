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
        public MedicalIncidentProfile()
        {
            //Chú ý cái này là enity mà có dấu _ thì phải map bằng tay
            CreateMap<MedicalIncident, MedicalIncidentDTOResponse>()
                .ForMember(dest => dest.NurseName, opt => opt.MapFrom(src => (src.nurse!.last_name + " "+ src.nurse.first_name)))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => (src.student!.last_name + " " + src.student.first_name)))
                .ForMember(dest => dest.MedicalIncidentId, opt => opt.MapFrom(src => src.medical_incident_id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.create_at))
                .ForMember(dest => dest.FollowUp, opt => opt.MapFrom(src => src.follow_up))
                .ForMember(dest => dest.SeverityLevel, opt => opt.MapFrom(src => src.severity_level));

            
            CreateMap<MedicalIncidentDTORequest, MedicalIncident>()
                .ForMember(dest => dest.create_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.follow_up, opt => opt.MapFrom(src => src.FollowUp))
                .ForMember(dest => dest.severity_level, opt => opt.MapFrom(src => src.SeverityLevel))
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.nurse_id, opt => opt.MapFrom(src => src.NurseId))
                .ForMember(dest => dest.medical_incident_id, opt => opt.Ignore());
        }
    }
}
