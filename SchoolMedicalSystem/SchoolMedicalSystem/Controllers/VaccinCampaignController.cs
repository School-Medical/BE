using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinCampaignController : ControllerBase
    {
        private readonly IVaccinCampaignService _vaccinCampaignService;
        public VaccinCampaignController(IVaccinCampaignService vaccinCampaignService)
        {
            _vaccinCampaignService = vaccinCampaignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVaccinCampaigns()
        {
            var campaigns = await _vaccinCampaignService.GetAllVaccinCampaignsAsync();
            return Ok(new ApiResponse<IEnumerable<VaccinCampaignDTOResponse>>("Success", campaigns, 200));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVaccinCampaignById(int id)
        {
            var campaign = await _vaccinCampaignService.GetVaccinCampaignByIdAsync(id);
            if (campaign == null)
            {
                return NotFound(new ApiResponse<object>("No campaigns found", null, 404));
            }
            return Ok(new ApiResponse<VaccinCampaignDTOResponse>("Success", campaign, 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVaccinCampaign([FromBody] VaccinCampaignDTORequest vaccinCampaign)
        {
            if (vaccinCampaign == null)
            {
                return BadRequest("Vaccin campaign cannot be null");
            }
            var createdCampaign = await _vaccinCampaignService.CreateVaccinCampaignAsync(vaccinCampaign);
            if (createdCampaign == null)
            {
                return BadRequest(new ApiResponse<object>("Failed to create vaccin campaign", new List<object>() { "Sai rồi kìa" }));
            }
            return Ok(new ApiResponse<VaccinCampaignDTOResponse>("Create success", createdCampaign, 200));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVaccinCampaign(int id, [FromBody] VaccinCampaignDTORequest vaccinCampaign)
        {
            if (vaccinCampaign == null)
            {
                return BadRequest("Vaccin campaign cannot be null");
            }

            var updated = await _vaccinCampaignService.UpdateVaccinCampaignAsync(id, vaccinCampaign);
            if (updated == null)
            {
                return NotFound(new ApiResponse<object>("No campaigns found", null, 404));
            }
            return Ok(new ApiResponse<VaccinCampaignDTOResponse>("Update success", updated, 200));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVaccinCampaign(int id)
        {
            var deleted = await _vaccinCampaignService.DeleteVaccinCampaignAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<object>("No campaigns found", null, 404));
            }
            return NoContent();

        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetVaccinCampaignsWithPaging(int pageSize = 10, int pageNumber = 1)
        {
            var campaigns = await _vaccinCampaignService.GetVaccinCampaignsPaginatedAsync(pageSize,pageNumber);
            if (campaigns == null)
            {
                return NotFound(new ApiResponse<object>("No campaigns found", null, 404));
            }
            return Ok(new ApiResponse<PaginatedResponse<VaccinCampaignDTOResponse>>("Success", campaigns, 200));

        }
    }
}
