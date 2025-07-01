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
        private readonly ILogger<MedicationsController> _logger;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMedication([FromBody] MedicationRequest request)
        {
            try
            {
                var result = await _medicationService.AddAsync(request);
                return CreatedAtAction(nameof(GetByName), new { name = result.MedicineName }, result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
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
        public async Task<IActionResult> Update([FromBody] MedicationRequest request)
        {
            var updated = await _medicationService.UpdateAsync(request);
            return Ok(updated);
        }

    }
}
