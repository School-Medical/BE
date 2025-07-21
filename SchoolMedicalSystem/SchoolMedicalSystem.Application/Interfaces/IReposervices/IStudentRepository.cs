using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentById(int id);

        Task<IEnumerable<Student>> GetStudentByName(string name);
        Task<Student?> GetStudentByStudentCode(string studentCode);
        Task<List<Student?>> GetStudentByStudentName(string studentName);
        Task<List<Student>> GetStudentByParentIdAsync(int parentId);

        Task<List<Student?>> GetStudentsByClassId(int classId);

        Task<List<Student>> GetAllStudents(int pageSize, int pageNumber);

        Task<List<Student>> GetAll();

        Task<Student> AddStudent(Student student);

        Task<Student> UpdateStudent(Student student);

        Task<List<Student>> AddStudentsToClass(List<Student> students, int classId);

        Task<List<Student>> GetAll();

    }
}
