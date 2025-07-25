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
    public class HealthCheckConfirmationProfile : Profile
    {
        public HealthCheckConfirmationProfile()
        {
            // Map from Request DTO to Entity
            CreateMap<HealthCheckConfirmationRequest, HealthCheckConfirmation>()
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.health_check_id, opt => opt.MapFrom(src => src.health_check_id))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.submit_at, opt => opt.Ignore()); // sẽ được set bên trong service

            // Map from Entity to Response DTO
            CreateMap<HealthCheckConfirmation, HealthCheckConfirmationResponse>()
                .ForMember(dest => dest.HealthCheckConfirmationId, opt => opt.MapFrom(src => src.hc_confirmation_id))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent_id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.HealthCheckId, opt => opt.MapFrom(src => src.health_check_id))
                .ForMember(dest => dest.SubmitAt, opt => opt.MapFrom(src => src.submit_at))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));
        }
    }
}
