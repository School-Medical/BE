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
            CreateMap<StudentParent, StudentParentDTORequest>()
                .ForMember(dest => dest.StudentParentId, opt => opt.MapFrom(src => src.student_parent_id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.student_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id));
        }
    }
 }
