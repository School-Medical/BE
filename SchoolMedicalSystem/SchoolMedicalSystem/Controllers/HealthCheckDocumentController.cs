using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckDocumentController : ControllerBase
    {
        private readonly IHealthCheckDocumentService _service;

        public HealthCheckDocumentController(IHealthCheckDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllHealthCheckDocumentsAsync();
            return Ok(new ApiResponse<IEnumerable<HealthCheckDocumentResponse>>("Success", result, 200));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetHealthCheckDocumentByIdAsync(id);
            return Ok(new ApiResponse<HealthCheckDocumentResponse>("Success", result, 200));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthCheckDocumentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>("Validation failed", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage), 400));

            var result = await _service.CreateHealthCheckDocumentAsync(request);
            return Ok(new ApiResponse<HealthCheckDocumentResponse>("Created successfully", result, 201));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HealthCheckDocumentRequest request)
        {
            var result = await _service.UpdateHealthCheckDocumentAsync(id, request);
            return Ok(new ApiResponse<HealthCheckDocumentResponse>("Updated successfully", result, 200));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var success = await _service.DeleteHealthCheckDocumentAsync(id);
        //    if (!success)
        //        return NotFound(new ApiResponse<object>("Document not found", null, 404));

        //    return Ok(new ApiResponse<string>("Deleted successfully", null, 200));
        //}

        [HttpGet("by-healthcheck/{healthCheckId}")]
        public async Task<IActionResult> GetByHealthCheckId(int healthCheckId)
        {
            var result = await _service.GetDocumentsByHealthCheckIdAsync(healthCheckId);
            return Ok(new ApiResponse<IEnumerable<HealthCheckDocumentResponse>>("Success", result, 200));
        }

        [HttpGet("by-confirmation/{confirmationId}")]
        public async Task<IActionResult> GetByConfirmationId(int confirmationId)
        {
            var result = await _service.GetDocumentsByConfirmationIdAsync(confirmationId);
            return Ok(new ApiResponse<IEnumerable<HealthCheckDocumentResponse>>("Success", result, 200));
        }

        [HttpGet("student/{studentId}/healthcheck/{healthCheckId}")]
        public async Task<IActionResult> GetByStudentAndHealthCheck(int studentId, int healthCheckId)
        {
            var result = await _service.GetDocumentByStudentAndHealthCheckId(studentId, healthCheckId);
            return Ok(new ApiResponse<HealthCheckConfirmationResponse>("Success", result, 200));
        }
    }
}
