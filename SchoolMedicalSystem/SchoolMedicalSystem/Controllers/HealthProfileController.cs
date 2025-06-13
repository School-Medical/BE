using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/students/[controller]")]
    [ApiController]
    public class HealthProfileController : ControllerBase
    {
        private readonly IHealthProfileService _healthProfileService;

        public HealthProfileController(IHealthProfileService healthProfileService)
        {
            _healthProfileService = healthProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHealthProfiles([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {
                if (pageSize <= 0 || pageNumber <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid pagination parameters", 
                        new List<string> { "PageSize and PageNumber must be greater than 0" }, 400));
                }

                if(pageSize > 100)
                {
                    return BadRequest(new ApiResponse<object>("Page size too large", 
                        new List<string> { "PageSize cannot exceed 100" }, 400));
                }

                var healthProfiles = await _healthProfileService.GetAllAsync(pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<HealthProfileDTOResponse>>("Health profiles retrieved successfully.", healthProfiles, 200));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthProfileById(int id)
        {
            try
            {
                var healthProfile = await _healthProfileService.GetByIdAsync(id);
                if (healthProfile == null)
                {
                    return NotFound(new ApiResponse<object>("Health profile not found.", null, 404));
                }
                return Ok(new ApiResponse<HealthProfileDTOResponse>("Health profile retrieved successfully.", healthProfile, 200));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
