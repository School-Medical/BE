using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Mappers
{
    public class StudentParentProfile : Profile
    {
        public StudentParentProfile()
        {
            CreateMap<StudentParentDTORequest, StudentParent>()
                .ForMember(dest => dest.student_id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId));
        }
    }
 }
