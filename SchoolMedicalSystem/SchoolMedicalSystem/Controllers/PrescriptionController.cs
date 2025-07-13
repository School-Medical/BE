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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPrescriptionMedicineService _prescriptionMedicineService;

        public PrescriptionController(IPrescriptionService prescriptionService, IPrescriptionMedicineService prescriptionMedicineService)
        {
            _prescriptionService = prescriptionService;
            _prescriptionMedicineService = prescriptionMedicineService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDTORequest request)
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

                var prescriptionResult = await _prescriptionService.AddAsync(request);
                var response = new
                {
                    Prescription = prescriptionResult,
                };

                return Ok(new ApiResponse<object>("Prescription and medicines created successfully", response, 201));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionDTORequest request)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID", new List<string> { "ID must be greater than 0" }, 400));
                }

                request.PrescriptionId = id;

                var result = await _prescriptionService.UpdateAsync(request);
                if (!result)
                {
                    return NotFound(new ApiResponse<object>("Prescription not found", new List<string> { $"No prescription found with ID: {id}" }, 404));
                }

                return Ok(new ApiResponse<object>("Prescription and medicines updated successfully", result, 200));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid ID", new List<string> { "ID must be greater than 0" }, 400));
                }

                var prescription = await _prescriptionService.GetByIdAsync(id);
                if (prescription == null)
                {
                    return NotFound(new ApiResponse<object>("Prescription not found", new List<string> { $"No prescription found with ID: {id}" }, 404));
                }

                var prescriptionMedicines = await _prescriptionMedicineService.GetByPrescriptionId(id);
                prescription.PrescriptionMedicines = prescriptionMedicines;
                var response = new
                {
                    Prescription = prescription,
                };

                return Ok(new ApiResponse<object>("Prescription fetched successfully", response, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }
    }
}
