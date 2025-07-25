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
    public class HealthCheckDocumentProfile : Profile
    {
        public HealthCheckDocumentProfile()
        {
            // Entity -> Response
            CreateMap<HealthCheckDocument, HealthCheckDocumentResponse>()
                .ForMember(dest => dest.HealthCheckDocumentId, opt => opt.MapFrom(src => src.hc_document_id))
                .ForMember(dest => dest.CheckAt, opt => opt.MapFrom(src => src.check_at))
                .ForMember(dest => dest.WeightIndex, opt => opt.MapFrom(src => src.weight_index))
                .ForMember(dest => dest.HeightIndex, opt => opt.MapFrom(src => src.height_index))
                .ForMember(dest => dest.VisionIndex, opt => opt.MapFrom(src => src.vision_index))
                .ForMember(dest => dest.HearingIndex, opt => opt.MapFrom(src => src.hearing_index))
                .ForMember(dest => dest.BloodPressureIndex, opt => opt.MapFrom(src => src.blood_pressure_index))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.HealthCheckId, opt => opt.MapFrom(src => src.health_check_id))
                .ForMember(dest => dest.HealthCheckConfirmId, opt => opt.MapFrom(src => src.health_check_confirm_id))
                .ForMember(dest => dest.NurseId, opt => opt.MapFrom(src => src.nurse_id));

            // Request -> Entity
            CreateMap<HealthCheckDocumentRequest, HealthCheckDocument>()
                .ForMember(dest => dest.check_at, opt => opt.MapFrom(src => src.CheckAt))
                .ForMember(dest => dest.weight_index, opt => opt.MapFrom(src => src.WeightIndex))
                .ForMember(dest => dest.height_index, opt => opt.MapFrom(src => src.HeightIndex))
                .ForMember(dest => dest.vision_index, opt => opt.MapFrom(src => src.VisionIndex))
                .ForMember(dest => dest.hearing_index, opt => opt.MapFrom(src => src.HearingIndex))
                .ForMember(dest => dest.blood_pressure_index, opt => opt.MapFrom(src => src.BloodPressureIndex))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.health_check_id, opt => opt.MapFrom(src => src.HealthCheckId))
                .ForMember(dest => dest.health_check_confirm_id, opt => opt.MapFrom(src => src.HealthCheckConfirmId))
                .ForMember(dest => dest.nurse_id, opt => opt.MapFrom(src => src.NurseId));
        }
    }
}
