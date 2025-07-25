using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckConfirmationController : ControllerBase
    {
        private readonly IHealthCheckConfirmationService _service;
        private readonly ILogger<HealthCheckConfirmationController> _logger;

        public HealthCheckConfirmationController(IHealthCheckConfirmationService service, ILogger<HealthCheckConfirmationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHealthCheckConfirmations()
        {
            try
            {
                var result = await _service.GetAllHealthCheckConfirmationsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all health check confirmations");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthCheckConfirmationById(int id)
        {
            try
            {
                var result = await _service.GetHealthCheckConfirmationByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Health check confirmation with id {id} not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting health check confirmation with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHealthCheckConfirmation(int id, [FromBody] HealthCheckConfirmationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.UpdateHealthCheckConfirmationAsync(id, request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating health check confirmation with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("student/{studentId}/health-check/{healthCheckId}")]
        public async Task<IActionResult> GetHealthCheckConfirmationByStudent(int studentId, int healthCheckId)
        {
            try
            {
                var result = await _service.GetHealthCheckConfirmationByStudentIdAsync(studentId, healthCheckId);
                if (result == null)
                {
                    return NotFound($"Health check confirmation for student {studentId} and health check {healthCheckId} not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting health check confirmation for student {studentId} and health check {healthCheckId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("health-check/{healthCheckId}")]
        public async Task<IActionResult> GetHealthCheckConfirmationsByHealthCheck(int healthCheckId)
        {
            try
            {
                var result = await _service.GetHealthCheckConfirmationsByHealthCheckIdAsync(healthCheckId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting health check confirmations for health check {healthCheckId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetHealthCheckConfirmationsByParent(int parentId)
        {
            try
            {
                var result = await _service.GetHealthCheckConfirmationsByParentIdAsync(parentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting health check confirmations for parent {parentId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
