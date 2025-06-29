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
    public class StudentParentsRepository : IStudentParentsRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        public StudentParentsRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CheckParentValid(int studentId, int parentId)
        {
            return await _dbContext.StudentParents
                .AnyAsync(sp => sp.student_id == studentId && sp.user_id == parentId);
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentParent> CreateAsync(StudentParent studentParents)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentParent>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentParent>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<StudentParent?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentParent?> GetStudentParentByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(StudentParent studentParents)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StudentParent>> GetStudentParentsByParentIdAsync(int parentId)
        {
            return await _dbContext.StudentParents.Include(sp => sp.student)
                .Where(sp => sp.user_id == parentId)
                .ToListAsync();
        }
    }
}
