using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class MedicationProfile : Profile
    {
        public MedicationProfile()
        {
            CreateMap<Medication, MedicationResponse>()
                .ForMember(dest => dest.MedicationId, opt => opt.MapFrom(src => src.medication_id))
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.medicine_name ?? string.Empty))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity ?? 0))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.unit ?? string.Empty))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.GivenDoseId, opt => opt.MapFrom(src => src.given_dose_id));
        }
    }
}
