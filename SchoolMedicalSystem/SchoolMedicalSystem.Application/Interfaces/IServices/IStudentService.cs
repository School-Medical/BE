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
    public interface IStudentService
    {
        Task<StudentDTOResponse> GetStudentByIdAsync(int id);
        Task<StudentDTOResponse> GetStudentByStudentCodeAsync(string studentCode);
        Task<List<StudentDTOResponse>> GetStudentByStudentNameAsync(string studentName);
        Task<PaginatedResponse<StudentDTOResponse>> GetStudentsByClassIdAsync(int classId, int pageSize, int pageNumber);
        Task<PaginatedResponse<StudentDTOResponse>> GetAllStudentsAsync(int pageSize, int pageNumber);
        //Task<List<StudentAddDTORequest>> GetAll();
        Task<StudentDTOResponse> AddStudentAsync(StudentAddDTORequest student);
        Task<StudentDTOResponse> UpdateStudentAsync(int id, StudentUpdateDTORequest student);
        Task<List<StudentDTOResponse>> AddStudentsToClassAsync(List<StudentAddDTORequest> students, int classId);
        Task<List<StudentDTOResponse>> GetAll();
    }
}
