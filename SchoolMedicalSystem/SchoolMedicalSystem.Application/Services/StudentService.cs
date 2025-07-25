using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
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

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<StudentDTOResponse> AddStudentAsync(StudentAddDTORequest student)
        {
            try
            {
                if (student == null)
                {
                    _logger.LogWarning("Attempted to add a null student");
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");
                }

                var entity = await _unitOfWork.Students.AddStudent(_mapper.Map<Student>(student));
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<StudentDTOResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student: {StudentName}", student.FirstName + " " + student.LastName);
                throw;
            }
        }

        public async Task<List<StudentDTOResponse>> AddStudentsToClassAsync(List<StudentAddDTORequest> students, int classId)
        {
            try
            {
                var studentEntities = _mapper.Map<List<Student>>(students);
                if (studentEntities == null || studentEntities.Count == 0)
                {
                    _logger.LogWarning("Attempted to add an empty list of students to class ID: {ClassId}", classId);
                    throw new ArgumentException("Student list cannot be null or empty.", nameof(students));
                }

                var addedStudents = await _unitOfWork.Students.AddStudentsToClass(studentEntities, classId);
                if (addedStudents == null || addedStudents.Count == 0)
                {
                    _logger.LogWarning("No students were added to class ID: {ClassId}", classId);
                    return null;
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<List<StudentDTOResponse>>(addedStudents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding students to class ID: {ClassId}", classId);
                throw;
            }
        }

        public async Task<List<StudentDTOResponse>> GetAll()
        {
            var students = await _unitOfWork.Students.GetAll();
            var result = _mapper.Map<List<StudentDTOResponse>>(students);
            return result;
        }

        public async Task<PaginatedResponse<StudentDTOResponse>> GetAllStudentsAsync(int pageSize, int pageNumber)
        {
            try
            {
                var students = await _unitOfWork.Students.GetAllStudents(pageSize, pageNumber);
                var totalPages = (int)Math.Ceiling((double)students.Count / pageSize);
                return new PaginatedResponse<StudentDTOResponse> {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = students.Count,
                    TotalPages = totalPages,
                    Items = _mapper.Map<List<StudentDTOResponse>>(students),
                    //HasPreviousPage = pageNumber > 1,
                    //HasNextPage = pageNumber < totalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all students");
                throw;
            }
        }

        public async Task<StudentDTOResponse> GetStudentByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Students.GetStudentById(id);
                if (entity == null)
                {
                    _logger.LogWarning("Student with ID {Id} not found", id);
                    throw new KeyNotFoundException($"Student with ID {id} not found");
                }
                
                return _mapper.Map<StudentDTOResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student with ID: {Id}", id);
                throw;
            }
        }

        public async Task<StudentDTOResponse> GetStudentByStudentCodeAsync(string studentCode)
        {
            try
            {
                if (string.IsNullOrEmpty(studentCode))
                {
                    _logger.LogWarning("Student code is null or empty");
                    throw new ArgumentException("Student code cannot be null or empty", nameof(studentCode));
                }

                var student = await _unitOfWork.Students.GetStudentByStudentCode(studentCode);

                if (student == null)
                {
                    _logger.LogWarning("Student with code {StudentCode} not found", studentCode);
                    throw new KeyNotFoundException($"Student with code {studentCode} not found");
                }

                return _mapper.Map<StudentDTOResponse>(student);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student by code: {StudentCode}", studentCode);
                throw;
            }
        }

        public async Task<List<StudentDTOResponse>> GetStudentByStudentNameAsync(string studentName)
        {
            try
            {
                if (string.IsNullOrEmpty(studentName))
                {
                    _logger.LogWarning("Student name is null or empty");
                    return null;
                }

                var students = await _unitOfWork.Students.GetStudentByStudentName(studentName);

                if (students == null || students.Count == 0)
                {
                    _logger.LogWarning("No students found with name: {StudentName}", studentName);
                    return null;
                }

                return _mapper.Map<List<StudentDTOResponse>>(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students by name: {StudentName}", studentName);
                throw;
            }
        }

        public async Task<PaginatedResponse<StudentDTOResponse>> GetStudentsByClassIdAsync(int classId, int pageSize, int pageNumber)
        {
            try
            {
                var students = await _unitOfWork.Students.GetStudentsByClassId(classId);

                if (students == null || students.Count == 0)
                {
                    _logger.LogWarning("No students found for class ID: {ClassId}", classId);
                    return new PaginatedResponse<StudentDTOResponse>
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalItems = 0,
                        TotalPages = 0,
                        Items = new List<StudentDTOResponse>(),
                        //HasPreviousPage = false,
                        //HasNextPage = false
                    };
                }

                var totalItems = students.Count;
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                var pagedStudents = students
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return new PaginatedResponse<StudentDTOResponse>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = _mapper.Map<List<StudentDTOResponse>>(pagedStudents),
                    //HasPreviousPage = pageNumber > 1,
                    //HasNextPage = pageNumber < totalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students for class ID: {ClassId}", classId);
                throw;
            }
        }


        public async Task<StudentDTOResponse> UpdateStudentAsync(int id, StudentUpdateDTORequest student)
        {
            try
            {
                if (student == null)
                {
                    _logger.LogWarning("Attempted to update a null student with ID: {Id}", id);
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");
                }

                var existingStudent = await _unitOfWork.Students.GetStudentById(id);
                if (existingStudent == null)
                {
                    _logger.LogWarning("Student with ID {Id} not found", id);
                    throw new KeyNotFoundException($"Student with ID {id} not found");
                }

                // Map vào thực thể đã tồn tại (để giữ nguyên student_code)
                _mapper.Map(student, existingStudent);

                await _unitOfWork.Students.UpdateStudent(existingStudent);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<StudentDTOResponse>(existingStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student with ID: {Id}", id);
                throw;
            }
        }

    }
}
