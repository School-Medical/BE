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
            return await _dbContext.Students.Include(s => s._class)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _dbContext.Students.Include(s => s._class).FirstOrDefaultAsync(s => s.student_id.Equals(id));
        }

        public async Task<Student?> GetStudentByStudentCode(string studentCode)
        {
            return await _dbContext.Students.Include(s => s._class).FirstOrDefaultAsync(s => s.student_code!.Equals(studentCode));
        }

        // Nếu tên học sinh mà có dấu thì chưa làm được
        public async Task<List<Student?>> GetStudentByStudentName(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName)) return null;

            var normalizedName = studentName.Trim().ToLower();

            return await _dbContext.Students!
                .Where(s => (s.last_name + " " + s.first_name).ToLower().Contains(normalizedName)).ToListAsync();
        }

        public async Task<List<Student?>> GetStudentsByClassId(int classId)
        {
            return await _dbContext.Students.Include(s => s._class)
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
