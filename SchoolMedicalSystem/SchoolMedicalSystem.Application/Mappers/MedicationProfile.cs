using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class MedicationProfile : Profile
    {
        public MedicationProfile()
        {
            CreateMap<Medication, MedicationDTORespone>()
                .ForMember(dest => dest.MedicationId, opt => opt.MapFrom(src => src.medication_id))
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.medicine_name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.unit))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message));

        }
    }
}
