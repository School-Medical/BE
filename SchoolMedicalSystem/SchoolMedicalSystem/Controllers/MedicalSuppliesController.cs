using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolMedicalSystem.Controllers
{
    [Route("/medical-supplies")]
    [ApiController]
    public class MedicalSuppliesController : ControllerBase
    {
        private readonly IMedicalSuppliesService _medicalSuppliesService;
        public MedicalSuppliesController(IMedicalSuppliesService medicalSuppliesService)
        {
            _medicalSuppliesService = medicalSuppliesService;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllMedicalSupplies()
        {
            try
            {
                var result = await _medicalSuppliesService.GetAllAsync();
                return Ok(new ApiResponse<List<MedicalSuppliesDTOResponse>>("Get all medical supplies successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalSuppliesById(int id)
        {
            try
            {
                var result = await _medicalSuppliesService.GetByIdAsync(id);
                return Ok(new ApiResponse<MedicalSuppliesDTOResponse>("Get medical supply successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when accessing medical supply.", ex.Message , 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalSupplies([FromQuery] int userId, [FromBody] MedicalSuppliesDTORequest medicalSuppliesDTORequest)
        {
            try
            {
                if (userId <= 0) return BadRequest(new ApiResponse<string>("Invalid userId", new List<string> { "User ID is required." }, 400));
                var result = await _medicalSuppliesService.AddAsync(medicalSuppliesDTORequest, userId);
                return CreatedAtAction(nameof(GetMedicalSuppliesById), new { id = result.MedicalSupplyId }, new ApiResponse<MedicalSuppliesDTOResponse>("Add medical supply successfully!", result, 201));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when adding medical supply.", ex.Message , 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalSupplies(int id, [FromBody] MedicalSuppliesDTORequest medicalSuppliesDTORequest)
        {
            try
            {
                var result = await _medicalSuppliesService.UpdateAsync(id, medicalSuppliesDTORequest);
                return Ok(new ApiResponse<MedicalSuppliesDTOResponse>("Update medical supply successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when updating medical supply.", ex.Message, 500));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalSupplies(int id)
        {
            try
            {
                var result = await _medicalSuppliesService.DeleteAsync(id);
                if (result)
                {
                    return Ok(new ApiResponse<string>("Delete medical supply successfully!", "Medical supply deleted.", 200));
                }
                return NotFound(new ApiResponse<string>("Error when deleting medical supply.", new List<string> { "Something went wrong." }, 404));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when deleting medical supply.", ex.Message, 500));
            }
        }
    }
}
