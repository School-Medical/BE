using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IBlogRepository
    {
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog?> GetByIdAsync(int id);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<bool> UpdateAsync(Blog blog);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Blog>> GetAllWithPagingAsync(int pageSize, int pageNumber);
        Task<int> CountAsync();
    }
}
