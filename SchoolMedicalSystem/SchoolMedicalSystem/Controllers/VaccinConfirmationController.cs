using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;
using System.Security.Claims;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinConfirmationController : ControllerBase
    {
        private readonly IVaccinConfirmationService _vaccinConfirmationService;
        public VaccinConfirmationController(IVaccinConfirmationService vaccinConfirmationService)
        {
            _vaccinConfirmationService = vaccinConfirmationService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllVaccinConfirmations()
        {
            var result = await _vaccinConfirmationService.GetAllVaccinConfirmationsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccinConfirmationById(int id)
        {
            var result = await _vaccinConfirmationService.GetVaccinConfirmationByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Đợi JWT Authentication để lấy User ID từ Claims đã nha
        /// </summary>
        /// <param name="vaccinConfirmation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateVaccinConfirmation([FromBody] VaccinConfirmationDTORequest vaccinConfirmation)
        {
            if (vaccinConfirmation == null || vaccinConfirmation.StudentId == null)
            {
                return BadRequest("Invalid request data");
            }
            try
            {
                int parentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentException("Parent ID is required"));
                var result = await _vaccinConfirmationService.CreateVaccinConfirmationAsync(parentId, vaccinConfirmation);
                return Ok(new ApiResponse<VaccinConfirmationDTOResponse>("Vaccin Confirmation created successfully", result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccinConfirmation(int id, [FromBody] VaccinConfirmationDTORequest vaccinConfirmation)
        {
            if (vaccinConfirmation == null || vaccinConfirmation.StudentId == null)
            {
                return BadRequest("Invalid request data");
            }
            var updated = await _vaccinConfirmationService.UpdateVaccinConfirmationAsync(id, vaccinConfirmation);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccinConfirmation(int id)
        {
            var deleted = await _vaccinConfirmationService.DeleteVaccinConfirmationAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        //Code này ổn ko ta

        [HttpGet("paging")]
        public async Task<IActionResult> GetVaccinConfirmationsWithPaging(int pageSize = 10, int pageNumber = 1)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("Page size and number must be greater than zero.");
            }
            var result = await _vaccinConfirmationService.GetAllVaccinConfirmationsWithPagingAsync(pageSize, pageNumber);
            return Ok(new ApiResponse<PaginatedResponse<VaccinConfirmationDTOResponse>>("Data retrieved successfully", result));
        }
    }
}
