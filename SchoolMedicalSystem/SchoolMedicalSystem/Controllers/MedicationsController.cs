using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMedication([FromBody] MedicationRequest request)
        {
            var result = await _medicationService.AddAsync(request);
            return CreatedAtAction(nameof(GetByName), new { name = result.MedicineName }, result);
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicationResponse>>> GetAll()
        {
            var result = await _medicationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<MedicationResponse>> GetByName(string name)
        {
            var result = await _medicationService.GetByNameAsync(name);
            return Ok(result);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] MedicationRequest request)
        {
            if (!string.Equals(name, request.MedicineName, StringComparison.OrdinalIgnoreCase))
                return BadRequest("Tên trong URL và request body không khớp.");

            var updated = await _medicationService.UpdateAsync(request);
            return Ok(updated);
        }

    }
}
