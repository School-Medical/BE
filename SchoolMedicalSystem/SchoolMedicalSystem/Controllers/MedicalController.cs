using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{

    [Route("/medical")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly IMedicalService _medicalService;
        public MedicalController(IMedicalService medicalService)
        {
            _medicalService = medicalService;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllMedical()
        {
            try
            {
                var result = await _medicalService.GetAllAsync();
                return Ok(new ApiResponse<List<MedicalSuppliesDTOResponse>>("Get all medical successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalById(int id)
        {
            try
            {
                var result = await _medicalService.GetByIdAsync(id);
                return Ok(new ApiResponse<MedicalSuppliesDTOResponse>("Get medical successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when accessing medical supply.", ex.Message, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedical([FromQuery] int userId, [FromBody] MedicalSuppliesDTORequest medicalSuppliesDTORequest)
        {
            try
            {
                if (userId <= 0) return BadRequest(new ApiResponse<string>("Invalid userId", new List<string> { "User ID is required." }, 400));
                var result = await _medicalService.AddAsync(medicalSuppliesDTORequest, userId);
                return CreatedAtAction(nameof(GetMedicalById), new { id = result.MedicalSupplyId }, new ApiResponse<MedicalSuppliesDTOResponse>("Add medical successfully!", result, 201));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when adding medical supply.", ex.Message, 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedical(int id, [FromBody] MedicalSuppliesDTORequest medicalSuppliesDTORequest)
        {
            try
            {
                var result = await _medicalService.UpdateAsync(id, medicalSuppliesDTORequest);
                return Ok(new ApiResponse<MedicalSuppliesDTOResponse>("Update medical successfully!", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when updating medical.", ex.Message, 500));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedical(int id)
        {
            try
            {
                var result = await _medicalService.DeleteAsync(id);
                if (result)
                {
                    return Ok(new ApiResponse<string>("Delete medical successfully!", "Medical supply deleted.", 200));
                }
                return NotFound(new ApiResponse<string>("Error when deleting medical.", new List<string> { "Something went wrong." }, 404));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("Error when deleting medical.", ex.Message, 500));
            }
        }
    }
}
