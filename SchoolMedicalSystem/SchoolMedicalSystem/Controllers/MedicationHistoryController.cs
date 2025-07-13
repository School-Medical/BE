using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationHistoryController : ControllerBase
    {
        private readonly IMedicationHistoryService _medicationHistoryService;
        public MedicationHistoryController(IMedicationHistoryService medicationHistoryService)
        {
            _medicationHistoryService = medicationHistoryService;
        }
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetMedicationHistory(int studentId)
        {
            try
            {
                var history = await _medicationHistoryService.GetMedicationHistory(studentId);
                return Ok(new ApiResponse<List<MedicationHistoryDTOResponse>>("Medical history read successfully", history, 201));

                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>("Validation failed", ex, 400));
            }
        }
    }
}
