using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class MedicationHistoryProfile : Profile
    {
        public MedicationHistoryProfile()
        {
            CreateMap<GivenDose, MedicationHistoryDTOResponse>()
                .ForMember(dest => dest.GivenDoseId, opt => opt.MapFrom(src => src.given_dose_id))
                .ForMember(dest => dest.PatientCondition, opt => opt.MapFrom(src => src.patient_condition))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.create_at))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.NurseId, opt => opt.MapFrom(src => src.nurse_id))
                .ForMember(dest => dest.Medications, opt => opt.MapFrom(src => src.Medications));
        }
    }
}
