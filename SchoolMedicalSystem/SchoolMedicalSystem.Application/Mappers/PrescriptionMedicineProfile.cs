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
    public class PrescriptionMedicineProfile : Profile
    {
        public PrescriptionMedicineProfile()
        {
            CreateMap<PrescriptionMedicine, PrescriptionMedicineDTORespone>()
                .ForMember(dest => dest.PrescriptionMedicineId, opt => opt.MapFrom(src => src.prescription_medicine_id))
                .ForMember(dest => dest.PescriptionId, opt => opt.MapFrom(src => src.prescription_id))
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.medicine_id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity));
            CreateMap<PrescriptionMedicineDTORequest, PrescriptionMedicine>()
                .ForMember(dest => dest.prescription_medicine_id, opt => opt.MapFrom(src => src.PrescriptionMedicineId))
                .ForMember(dest => dest.prescription_id, opt => opt.MapFrom(src => src.PescriptionId))
                .ForMember(dest => dest.medicine_id, opt => opt.MapFrom(src => src.MedicineId))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
