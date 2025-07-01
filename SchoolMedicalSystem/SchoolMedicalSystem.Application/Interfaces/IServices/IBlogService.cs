using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDTOResponse>> GetAllBlogsAsync();
        Task<BlogDTOResponse?> GetBlogByIdAsync(int id);
        Task<BlogDTOResponse> CreateBlogAsync(BlogDTORequest blog);
        Task<bool> UpdateBlogAsync(int blogId, BlogDTORequest blog);
        Task<bool> DeleteBlogAsync(int id);
        Task<PaginatedResponse<BlogDTOResponse>> GetAllBlogsWithPagingAsync(int pageSize, int pageNumber);
    }
}
