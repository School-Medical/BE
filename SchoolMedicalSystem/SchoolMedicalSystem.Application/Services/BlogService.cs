using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogService> _logger;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public BlogService(IUnitOfWork unitOfWork, ILogger<BlogService> logger, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<IEnumerable<BlogDTOResponse>> GetAllBlogsAsync()
        {           
            return _mapper.Map<IEnumerable<BlogDTOResponse>>(await _unitOfWork.Blogs.GetAllAsync());
        }

        public async Task<BlogDTOResponse?> GetBlogByIdAsync(int id)
        {
            return _mapper.Map<BlogDTOResponse>(await _unitOfWork.Blogs.GetByIdAsync(id));
        }

        public async Task<BlogDTOResponse> CreateBlogAsync(BlogDTORequest blog)
        {
            var blogEntity = _mapper.Map<Blog>(blog);

            if (blog.ImageUrl != null)
            {
                blogEntity.image_url = await _cloudinaryService.UploadImageAsync(blog.ImageUrl);
            }

            var created = await _unitOfWork.Blogs.CreateAsync(blogEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogDTOResponse>(blogEntity);
        }

        public async Task<bool> UpdateBlogAsync(int blogId, BlogDTORequest blog)
        {
            var blogEntity = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog.ImageUrl != null)
            {
                blogEntity.image_url = await _cloudinaryService.UploadImageAsync(blog.ImageUrl);
            }

            _mapper.Map(blog, blogEntity);

            var updated = await _unitOfWork.Blogs.UpdateAsync(blogEntity);
            if (updated)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return updated;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var deleted = await _unitOfWork.Blogs.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }

        public async Task<PaginatedResponse<BlogDTOResponse>> GetAllBlogsWithPagingAsync(int pageSize, int pageNumber)
        {
            
            int totalItems = await _unitOfWork.Blogs.CountAsync();
            var pagedEntities = await _unitOfWork.Blogs.GetAllWithPagingAsync(pageSize, pageNumber);
            var result = _mapper.Map<List<BlogDTOResponse>>(pagedEntities);

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new PaginatedResponse<BlogDTOResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = result
            };
        }
    }
}
