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
    public class GivenDoseProfile : Profile
    {
        public GivenDoseProfile()
        {
            // Map từ Entity sang Response
            CreateMap<GivenDose, GivenDoseResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.given_dose_id))
                .ForMember(dest => dest.PatientCondition, opt => opt.MapFrom(src => src.patient_condition))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.create_at))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent_id))
                .ForMember(dest => dest.NurseId, opt => opt.MapFrom(src => src.nurse_id))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.student.first_name + " " + src.student.last_name))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.parent.first_name + " " + src.parent.last_name))
                .ForMember(dest => dest.NurseName, opt => opt.MapFrom(src => src.nurse.first_name + " " + src.nurse.last_name))
                .ForMember(dest => dest.ListMedication, opt => opt.MapFrom(src => src.Medications)); 


            // Map từ Request sang Entity
            CreateMap<GivenDoseRequest, GivenDose>()
                .ForMember(dest => dest.given_dose_id, opt => opt.Ignore()) // Bỏ qua ID khi tạo mới
                .ForMember(dest => dest.patient_condition, opt => opt.MapFrom(src => src.PatientCondition))
                .ForMember(dest => dest.create_at, opt => opt.Ignore()) // Set ở Service
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.parent_id, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.nurse_id, opt => opt.MapFrom(src => src.NurseId))
                .ForMember(dest => dest.Medications, opt => opt.Ignore()) // Gán ở Service nếu cần
                .ForMember(dest => dest.student, opt => opt.Ignore())
                .ForMember(dest => dest.parent, opt => opt.Ignore())
                .ForMember(dest => dest.nurse, opt => opt.Ignore());
        }
    }
}
