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
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;

        public StudentRepository(SchoolMedicalDbContext dbContext) => _dbContext = dbContext;

        public async Task<Student?> GetStudentById(int id)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s.student_id.Equals(id));
        }

        public async Task<Student?> GetStudentByStudentCode(string studentCode)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s.student_code!.Equals(studentCode));
        }

        
        public async Task<List<Student?>> GetStudentByStudentName(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName)) return null;

            var normalizedName = studentName.Trim().ToLower();

            return await _dbContext.Students
                .Where(s => (s.last_name + " " + s.first_name).ToLower().Contains(normalizedName)).ToListAsync();
        }

        public async Task<bool> CheckParentValid(int studentId, int parentId)
        {
            var student = await _dbContext.Students.Where(s => s.student_id == studentId && s.user_id == parentId)

                .FirstOrDefaultAsync();
            if (student == null) return false;
            return student.user_id == parentId;
        }

        public async Task<bool> CheckParentValid2(int studentId, int parentId)
        {
            return await _dbContext.StudentParents
                .AnyAsync(sp => sp.student_id == studentId && sp.user_id == parentId);
        }

    }
}
