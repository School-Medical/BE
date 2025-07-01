using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        private readonly SchoolMedicalDbContext _dbContext;

        public BlogRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _dbContext.Blogs.AddAsync(blog);
            return blog;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _dbContext.Blogs.FindAsync(id);
            if (existingEntity == null) return false;
            _dbContext.Remove(existingEntity);
            return true;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _dbContext.Blogs.ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.Blogs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _dbContext.Blogs.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Blog blog)
        {
            _dbContext.Update(blog);
            return true;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Blogs.CountAsync();
        }
    }
}
