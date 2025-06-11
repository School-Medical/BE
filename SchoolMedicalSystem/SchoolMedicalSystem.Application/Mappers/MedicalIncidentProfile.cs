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
            CreateMap<MedicalIncident, MedicalIncidentDTOResponse>()
                .ForMember(dest => dest.nurse_name, opt => opt.MapFrom(src => (src.nurse.first_name + src.nurse.last_name)))
                .ForMember(dest => dest.student_name, opt => opt.MapFrom(src => (src.student.first_name + src.student.last_name)));

            CreateMap<MedicalIncidentDTORequest, MedicalIncident>();
            CreateMap<Medicine, MedicalSuppliesDTOResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.medicine_name))
                .ForMember(dest => dest.Effect, opt => opt.MapFrom(src => src.medicine_effect))
                .ForMember(dest => dest.Usage, opt => opt.MapFrom(src => src.medicine_usage))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.medicine_type))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.expiry_date))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                .ForMember(dest => dest.BatchId, opt => opt.MapFrom(src => src.batch_id));

            CreateMap<MedicalSuppliesDTORequest, Medicine>()
                .ForMember(dest => dest.medicine_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.medicine_effect, opt => opt.MapFrom(src => src.Effect))
                .ForMember(dest => dest.medicine_usage, opt => opt.MapFrom(src => src.Usage))
                .ForMember(dest => dest.medicine_type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.expiry_date, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.image_url, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.medicine_id, opt => opt.Ignore());


        }
    }
}
