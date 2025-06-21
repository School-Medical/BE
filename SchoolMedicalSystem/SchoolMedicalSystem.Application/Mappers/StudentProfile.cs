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
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Define mappings here if needed
            CreateMap<Student, StudentDTOResponse>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(src => src.student_code))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.class_id));

            CreateMap<StudentDTORequest, Student>()
                .ForMember(dest => dest.student_code, opt => opt.MapFrom(src => src.StudentCode))
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.class_id, opt => opt.MapFrom(src => src.ClassId));
        }
    }
}
