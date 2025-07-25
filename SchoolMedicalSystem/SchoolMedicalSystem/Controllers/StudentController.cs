using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Get all students
        [HttpGet()]
        public async Task<IActionResult> GetAllStudents(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                if (pageSize <= 0 || pageNumber <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid pagination parameters",
                        new List<string> { "PageSize and PageNumber must be greater than 0" }, 400));
                }   

                if (pageSize > 100)
                {
                    return BadRequest(new ApiResponse<object>("Page size too large",
                       new List<string> { "PageSize cannot exceed 100" }, 400));
                }

                // Logic to get all students with pagination
                var result = await _studentService.GetAllStudentsAsync(pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<StudentDTOResponse>>("Data retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        // Get all students without pagination
        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudentsWithoutPaging()
        {
            try
            {
                var result = await _studentService.GetAll(); // Không có tham số phân trang
                return Ok(new ApiResponse<List<StudentDTOResponse>>("Data retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    "Internal server error",
                    new List<string> { ex.Message },
                    500));
            }
        }



        // Get student by ID
        [HttpGet("{id}")]
            public async Task<IActionResult> GetStudentById(int id)
            {
                try
                {
                    if (id <= 0)
                    {
                        return BadRequest(new ApiResponse<object>("Invalid ID",
                            new List<string> { "ID must be greater than 0" }, 400));
                    }

                    var student = await _studentService.GetStudentByIdAsync(id);
                    if (student == null)
                    {
                        return NotFound(new ApiResponse<object>("Student not found",
                            new List<string> { $"No student found with ID: {id}" }, 404));
                    }

                    return Ok(new ApiResponse<StudentDTOResponse>("Data retrieved successfully", student, 200));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
                }
            }

        // Add a new student
        [HttpPost()]
        public async Task<IActionResult> AddStudent([FromBody] StudentAddDTORequest studentDto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(new ApiResponse<object>("Validation failed", errors, 400));
                }
                // Logic to add the student
                var result = await _studentService.AddStudentAsync(studentDto);
                return Ok(new ApiResponse<StudentDTOResponse>("Student created successfully", result, 201));

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        // Update an existing student
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentUpdateDTORequest studentDto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID",
                        new List<string> { "ID must be greater than 0" }, 400));
                }
                // Logic to update the student
                var result = await _studentService.UpdateStudentAsync(id, studentDto);
                return Ok(new ApiResponse<StudentDTOResponse>("Student updated successfully", result, 200));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }
        // Add students to a class
        [HttpPost("list/{classId}")]
        public async Task<IActionResult> AddStudentsToClass(int classId, [FromBody] List<StudentAddDTORequest> studentDtos)
        {
            try
            {
                if (classId <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid class ID",
                        new List<string> { "Class ID must be greater than 0" }, 400));
                }

                if (studentDtos == null || studentDtos.Count == 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid input",
                        new List<string> { "Student list cannot be null or empty" }, 400));
                }

                // Logic to add students to the class
                var result = await _studentService.AddStudentsToClassAsync(studentDtos, classId);
                return Ok(new ApiResponse<List<StudentDTOResponse>>("Students added to class successfully", result, 201));
            }catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        // Get students by class ID
        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetStudentsByClassId(int classId, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                if (classId <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid class ID",
                        new List<string> { "Class ID must be greater than 0" }, 400));
                }
                if (pageSize <= 0 || pageNumber <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid pagination parameters",
                        new List<string> { "PageSize and PageNumber must be greater than 0" }, 400));
                }

                if (pageSize > 100)
                {
                    return BadRequest(new ApiResponse<object>("Page size too large",
                        new List<string> { "PageSize cannot exceed 100" }, 400));
                }

                // Logic to get students by class ID with pagination
                var result = await _studentService.GetStudentsByClassIdAsync(classId, pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<StudentDTOResponse>>("Data retrieved successfully", result, 200));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }
    }
}
