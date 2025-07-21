using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    /// <summary>
    /// Author : TinHT
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalIncidentController : ControllerBase
    {
        private readonly IMedicalIncidentService _medicalIncidentService;

        public MedicalIncidentController(IMedicalIncidentService medicalIncidentService)
        {
            _medicalIncidentService = medicalIncidentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalIncident([FromBody] MedicalIncidentDTORequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(new ApiResponse<object>("Validation failed", errors, 400));
                }

                var result = await _medicalIncidentService.AddAsync(request);
                return Ok(new ApiResponse<MedicalIncidentDTOResponse>("Medical incident created successfully", result, 201));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetMedicalIncidents([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {

                if (pageSize <= 0 || pageNumber <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid pagination parameters",
                        new List<string> { "PageSize and PageNumber must be greater than 0" }, 400));
                }

                if (pageSize > 100) // Giới hạn pageSize để tránh overload
                {
                    return BadRequest(new ApiResponse<object>("Page size too large",
                        new List<string> { "PageSize cannot exceed 100" }, 400));
                }

                var result = await _medicalIncidentService.GetAllAsync(pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<MedicalIncidentDTOResponse>>("Data retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalIncidentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID",
                        new List<string> { "ID must be greater than 0" }, 400));
                }

                var result = await _medicalIncidentService.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new ApiResponse<object>("Medical incident not found",
                        new List<string> { $"No medical incident found with ID: {id}" }, 404));
                }

                return Ok(new ApiResponse<MedicalIncidentDTOResponse>("Data retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpGet("student")]
        public async Task<IActionResult> GetMedicalIncidentByStudent([FromQuery] string? studentCode = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(studentCode) )
                {
                    return BadRequest(new ApiResponse<object>("Missing parameters",
                        new List<string> { "Either studentCode must be provided" }, 400));
                }

                var result = await _medicalIncidentService.GetByStudentCodeOrByNameAsync(studentCode!);

                if (result == null)
                {
                    return NotFound(new ApiResponse<object>("Medical incident not found",
                        new List<string> { "No medical incident found for the specified student" }, 404));
                }

                return Ok(new ApiResponse<List<MedicalIncidentDTOResponse>>("Data retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalIncident(int id, [FromBody] MedicalIncidentDTORequest request)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID",
                        new List<string> { "ID must be greater than 0" }, 400));
                }             

                var result = await _medicalIncidentService.UpdateAsync(id, request);
                return Ok(new ApiResponse<MedicalIncidentDTOResponse>("Medical incident updated successfully", result, 200));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalIncident(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID",
                        new List<string> { "ID must be greater than 0" }, 400));
                }

                var result = await _medicalIncidentService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new ApiResponse<object>("Medical incident not found",
                        new List<string> { $"No medical incident found with ID: {id}" }, 404));
                }

                return Ok(new ApiResponse<object>("Medical incident deleted successfully", null, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

    }
}
