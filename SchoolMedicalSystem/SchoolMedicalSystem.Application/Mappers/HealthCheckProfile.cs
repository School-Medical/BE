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
    public class HealthCheckProfile : Profile   
    {
        public HealthCheckProfile()
        {
            // Entity -> Response
            CreateMap<HealthCheck, HealthCheckResponse>()
                .ForMember(dest => dest.HealthCheckId, opt => opt.MapFrom(src => src.health_check_id))
                .ForMember(dest => dest.HealthCheckName, opt => opt.MapFrom(src => src.hc_name ?? string.Empty))
                .ForMember(dest => dest.HealthCheckDescription, opt => opt.MapFrom(src => src.hc_description ?? string.Empty))
                .ForMember(dest => dest.HealthCheckLocation, opt => opt.MapFrom(src => src.location ?? string.Empty))
                .ForMember(dest => dest.HealthCheckStart, opt => opt.MapFrom(src => src.start_at ?? DateTime.MinValue))
                .ForMember(dest => dest.HealthCheckEnd, opt => opt.MapFrom(src => src.end_at ?? DateTime.MinValue))
                .ForMember(dest => dest.RegisteredAt, opt => opt.MapFrom(src => src.register_start ?? DateTime.MinValue))
                .ForMember(dest => dest.RegisteredBy, opt => opt.MapFrom(src => src.register_close ?? DateTime.MinValue))
                .ForMember(dest => dest.HealthCheckStatus, opt => opt.MapFrom(src =>
                    src.status));

            // Request -> Entity
            CreateMap<HealthCheckRequest, HealthCheck>()
                .ForMember(dest => dest.hc_name, opt => opt.MapFrom(src => src.HealthCheckName))
                .ForMember(dest => dest.hc_description, opt => opt.MapFrom(src => src.HealthCheckDescription))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.HealthCheckLocation))
                .ForMember(dest => dest.start_at, opt => opt.MapFrom(src => src.StartAt))
                .ForMember(dest => dest.end_at, opt => opt.MapFrom(src => src.EndAt))
                .ForMember(dest => dest.register_start, opt => opt.MapFrom(src => src.RegisterStart))
                .ForMember(dest => dest.register_close, opt => opt.MapFrom(src => src.RegisterEnd));
        }
    }
}
