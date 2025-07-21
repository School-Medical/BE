using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/stdudentparent")]
    [ApiController]
    public class StudentParentController : ControllerBase
    {
        public readonly IStudentParentService _studentParentService;
        public StudentParentController(IStudentParentService studentParentService)
        {
            _studentParentService = studentParentService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] StudentParentDTORequest studentParentDto)
        {
            if (studentParentDto == null)
            {
                return BadRequest(new ApiResponse<object>("Student parent created failed", studentParentDto, 400));

            }
            var studentParent = await _studentParentService.AddAsync(studentParentDto);
            return Ok(new ApiResponse<object>("Student parent created successfully", studentParent, 201));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>("Invalid ID", null, 400));
            }
            var result = await _studentParentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<object>("Student parent not found", null, 404));
            }
            return Ok(new ApiResponse<object>("Student parent deleted successfully", null, 200));
        }
    }
}
