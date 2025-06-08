using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthProfileController : ControllerBase
    {
        private readonly IHealthProfileService _healthProfileService;

        public HealthProfileController(IHealthProfileService healthProfileService)
        {
            _healthProfileService = healthProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHealthProfiles()
        {
            try
            {
                var healthProfiles = await _healthProfileService.GetAllAsync();
                return Ok(new ApiResponse<List<HealthProfileDTOResponse>>("Health profiles retrieved successfully.", healthProfiles, 200));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
