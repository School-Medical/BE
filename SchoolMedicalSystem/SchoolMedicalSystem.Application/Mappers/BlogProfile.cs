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
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDTOResponse>()
                .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.blog_id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.update_at));

            CreateMap<BlogDTORequest, Blog>()              
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.update_at, opt => opt.MapFrom(src => src.UpdatedAt));

        }
    }
}
