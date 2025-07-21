using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckService _healthCheckService;
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(IHealthCheckService healthCheckService, ILogger<HealthCheckController> logger)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        /// <summary>
        /// Get all health checks
        /// </summary>
        /// <returns>List of health checks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HealthCheckResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllHealthChecks()
        {
            try
            {
                var result = await _healthCheckService.GetAllHealthChecksAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all health checks");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get health check by id
        /// </summary>
        /// <param name="id">Health check id</param>
        /// <returns>Health check</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthCheckById(int id)
        {
            try
            {
                var result = await _healthCheckService.GetHealthCheckByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Health check with id {id} not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting health check with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new health check
        /// </summary>
        /// <param name="healthCheckRequest">Health check data</param>
        /// <returns>Created health check</returns>
        [HttpPost]
        public async Task<IActionResult> CreateHealthCheck([FromBody] HealthCheckRequest healthCheckRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _healthCheckService.CreateHealthCheckAsync(healthCheckRequest);
                return CreatedAtAction(nameof(GetHealthCheckById), new { id = result.HealthCheckId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating health check");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Update health check
        /// </summary>
        /// <param name="id">Health check id</param>
        /// <param name="healthCheckRequest">Health check data</param>
        /// <returns>Updated health check</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHealthCheck(int id, [FromBody] HealthCheckRequest healthCheckRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _healthCheckService.UpdateHealthCheckAsync(id, healthCheckRequest);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating health check with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("/paging")]
        public async Task<IActionResult> GetAllHealthChecks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _healthCheckService.GetPagedHealthChecksAsync(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated health checks");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
