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
            return Ok(new ApiResponse<IEnumerable<VaccinConfirmationDTOResponse>>("Data retrieved successfully", result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccinConfirmationById(int id)
        {
            var result = await _vaccinConfirmationService.GetVaccinConfirmationByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>("No campaigns found", null, 404));
            }
            return Ok(new ApiResponse<VaccinConfirmationDTOResponse>("Data retrieved successfully", result));
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
                var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //TÔi lấy id từ Claims của JWT Authentication thì check vậy ok chưa
                if (!int.TryParse(idClaim, out int parentId) || parentId <= 0)
                {
                    return BadRequest(new ApiResponse<object>("You must login by parent account", null, 400));
                }
                var result = await _vaccinConfirmationService.CreateVaccinConfirmationAsync(parentId, vaccinConfirmation);
                return Ok(new ApiResponse<VaccinConfirmationDTOResponse>("Vaccin Confirmation created successfully", result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>(ex.Message, null, 400));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccinConfirmation(int id, [FromBody] VaccinConfirmationDTORequest vaccinConfirmation)
        {
            if (vaccinConfirmation == null || vaccinConfirmation.StudentId == null)
            {
                return BadRequest(new ApiResponse<object>("Invalid request data", null, 400));
            }
            var updated = await _vaccinConfirmationService.UpdateVaccinConfirmationAsync(id, vaccinConfirmation);
            if (updated != null)
            {
                return Ok(new ApiResponse<VaccinConfirmationDTOResponse>("Vaccin Confirmation updated successfully", updated));                
            }
            return NotFound(new ApiResponse<object>("No campaigns found to update", null, 404));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccinConfirmation(int id)
        {
            var deleted = await _vaccinConfirmationService.DeleteVaccinConfirmationAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<object>("No campaigns found to delete", null, 404));
            }
            return NoContent();
        }

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

        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetVaccinConfirmationByParentId()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out int parentId) || parentId <= 0)
            {
                return BadRequest(new ApiResponse<object>("You must login by parent account", null, 400));
            }
            var result = await _vaccinConfirmationService.GetVaccinConfirmationByParentIdAsync(parentId);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>("No confirmation found for this parent", null, 404));
            }
            return Ok(new ApiResponse<VaccinConfirmationDTOResponse>("Data retrieved successfully", result));
        }
}
