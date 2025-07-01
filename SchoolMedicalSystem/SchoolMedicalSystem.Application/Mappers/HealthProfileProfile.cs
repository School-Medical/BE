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
                .ForMember(dest => dest.WeightIndex, opt => opt.MapFrom(src => src.weight_index))
                .ForMember(dest => dest.HeightIndex, opt => opt.MapFrom(src => src.height_index))
                .ForMember(dest => dest.VisionIndex, opt => opt.MapFrom(src => src.vision_index))
                .ForMember(dest => dest.HearingIndex, opt => opt.MapFrom(src => src.hearing_index))
                .ForMember(dest => dest.BloodPressureIndex, opt => opt.MapFrom(src => src.blood_pressure_index))
                .ForMember(dest => dest.AllergyList, opt => opt.MapFrom(src => src.allergy_list))
                .ForMember(dest => dest.ChronicDisease, opt => opt.MapFrom(src => src.chronic_disease))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.medical_history))
                .ForMember(dest => dest.MedicationInUse, opt => opt.MapFrom(src => src.medication_in_use))
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.blood_group))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.gender))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.birthday))
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.update_at))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.student!.last_name + " " + src.student.first_name))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(src => src.student!.student_code))
                .ForMember(dest => dest.ParentPhoneNumber, opt => opt.MapFrom(src => src.student!.parent_phone_number))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.student!.image_url))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.student!.StudentParents.
                Where(sp => sp.student_id == src.student_id).FirstOrDefault()!.user_id))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.student!.class_id))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.student!._class!.class_name));
                

            CreateMap<HealthProfileDTORequest, HealthProfile>()
                .ForMember(dest => dest.weight_index, opt => opt.MapFrom(src => src.WeightIndex))
                .ForMember(dest => dest.height_index, opt => opt.MapFrom(src => src.HeightIndex))
                .ForMember(dest => dest.vision_index, opt => opt.MapFrom(src => src.VisionIndex))
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
