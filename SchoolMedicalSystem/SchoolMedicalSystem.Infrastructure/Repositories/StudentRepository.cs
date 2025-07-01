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

        public async Task<Student> AddStudent(Student student)
        {
            await _dbContext.AddAsync(student);
            return student;
        }

        public async Task<List<Student>> AddStudentsToClass(List<Student> students, int classId)
        {
            if (students == null || students.Count == 0)
                throw new ArgumentException("Student list cannot be null or empty.");

            foreach (var student in students)
            {
                student.class_id = classId;
                _dbContext.Students.Update(student); 
            }

            return students;
        }

        public async Task<List<Student>> GetAllStudents(int pageSize, int pageNumber)
        {
            return await _dbContext.Students.Include(s => s._class).Include(s => s.StudentParents)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _dbContext.Students.Include(s => s._class)
                .Include(s => s.StudentParents).FirstOrDefaultAsync(s => s.student_id == id);
        }

        public async Task<Student?> GetStudentByStudentCode(string studentCode)
        {
            return await _dbContext.Students.Include(s => s._class).Include(s => s.StudentParents).FirstOrDefaultAsync(s => s.student_code!.Equals(studentCode));
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

        public async Task<List<Student>> GetStudentByParentIdAsync(int parentId)
        {
            return await _dbContext.Students.Where(s => s.user_id == parentId).ToListAsync();
        }


        public async Task<List<Student?>> GetStudentsByClassId(int classId)
        {
            return await _dbContext.Students.Include(s => s._class).Include(s => s.StudentParents)
                .Where(stu => stu.class_id == classId)
                .ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _dbContext.Update(student);
            return student;
        }

    }
}
