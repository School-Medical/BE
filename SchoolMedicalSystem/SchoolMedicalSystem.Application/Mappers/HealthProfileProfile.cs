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
    public class HealthProfileProfile : Profile
    {
        public HealthProfileProfile()
        {
            CreateMap<HealthProfile, HealthProfileDTOResponse>()

                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.student!.last_name + " " + src.student.first_name));

            CreateMap<HealthProfileDTORequest, HealthProfile>()
                .ForMember(dest => dest.weight_index, opt => opt.MapFrom(src => src.WeightIndex))
                .ForMember(dest => dest.hearing_index, opt => opt.MapFrom(src => src.HearingIndex))
                .ForMember(dest => dest.blood_pressure_index, opt => opt.MapFrom(src => src.BloodPressureIndex))
                .ForMember(dest => dest.allergy_list, opt => opt.MapFrom(src => src.AllergyList))
                .ForMember(dest => dest.chronic_disease, opt => opt.MapFrom(src => src.ChronicDisease))
                .ForMember(dest => dest.medical_history, opt => opt.MapFrom(src => src.MedicalHistory))
                .ForMember(dest => dest.medication_in_use, opt => opt.MapFrom(src => src.MedicationInUse))
                .ForMember(dest => dest.blood_group, opt => opt.MapFrom(src => src.BloodGroup))
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.update_at, opt => opt.MapFrom(src => src.UpdateAt))
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId));
        }
    }
}
