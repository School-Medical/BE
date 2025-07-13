using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<Prescription, PrescriptionDTORespone>()
                .ForMember(dest => dest.PrescriptionId, opt => opt.MapFrom(src => src.prescription_id))
                .ForMember(dest => dest.Instruction, opt => opt.MapFrom(src => src.instruction))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.create_at))
                .ForMember(dest => dest.MedicalIncidentId, opt => opt.MapFrom(src => src.medical_incident_id));
            CreateMap<PrescriptionDTORequest, Prescription>()
                .ForMember(dest => dest.prescription_id, opt => opt.MapFrom(src => src.PrescriptionId))
                .ForMember(dest => dest.instruction, opt => opt.MapFrom(src => src.Instruction))
                .ForMember(dest => dest.create_at, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.medical_incident_id, opt => opt.MapFrom(src => src.MedicalIncidentId));
            CreateMap<AddPrescriptionDTORequest, Prescription>()
                .ForMember(dest => dest.instruction, opt => opt.MapFrom(src => src.Instruction))
                .ForMember(dest => dest.create_at, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.medical_incident_id, opt => opt.MapFrom(src => src.MedicalIncidentId));
        }
    }
}
