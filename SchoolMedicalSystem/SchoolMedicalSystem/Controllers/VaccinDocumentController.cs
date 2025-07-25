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
    public class VaccinDocumentController : ControllerBase
    {
        private readonly IVaccinDocumentService _vaccinDocumentService;
        public VaccinDocumentController(IVaccinDocumentService vaccinDocumentService)
        {
            _vaccinDocumentService = vaccinDocumentService;
        }

        /*
        [HttpPost("upload")]
        public async Task<IActionResult> UploadVaccinDocument([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            try
            {
                var result = await _vaccinDocumentService.UploadVaccinDocumentAsync(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadVaccinDocument(int id)
        {
            try
            {
                var fileStream = await _vaccinDocumentService.DownloadVaccinDocumentAsync(id);
                if (fileStream == null)
                {
                    return NotFound($"Vaccin Document with ID {id} not found for download.");
                }
                return File(fileStream, "application/pdf", $"VaccinDocument_{id}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading document: {ex.Message}");
            }
        }*/

        [HttpGet]
        public async Task<IActionResult> GetAllVaccinDocuments()
        {
            try
            {
                var documents = await _vaccinDocumentService.GetAllVaccinDocumentsAsync();
                return Ok(new ApiResponse<IEnumerable<VaccinDocumentDTOResponse>>("Data retrieved successfully", documents));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving documents: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccinDocumentById(int id)
        {
            try
            {
                var document = await _vaccinDocumentService.GetVaccinDocumentByIdAsync(id);
                if (document == null)
                {
                    return NotFound(new ApiResponse<object>($"Vaccin Document with ID {id} not found for update.", null, 404));
                }
                return Ok(new ApiResponse<VaccinDocumentDTOResponse>("Vaccin Document retrieved successfully", document));
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ApiResponse<object>(knfEx.Message, null, 404));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving document: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVaccinDocument([FromBody] VaccinDocumentDTORequest vaccinDocument)
        {
            if (vaccinDocument == null)
            {
                return BadRequest("Vaccin Document request cannot be null.");
            }
            try
            {
                var createdDocument = await _vaccinDocumentService.CreateVaccinDocumentAsync(vaccinDocument);
                return Ok(new ApiResponse<VaccinDocumentDTOResponse>("Vaccin Document created successfully", createdDocument));
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating document: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccinDocument(int id, [FromBody] VaccinDocumentDTORequest vaccinDocument)
        {
            if (vaccinDocument == null)
            {
                return BadRequest("Vaccin Document request cannot be null.");
            }
            try
            {
                var updated = await _vaccinDocumentService.UpdateVaccinDocumentAsync(id, vaccinDocument);
                if (updated == null)
                {
                    return NotFound(new ApiResponse<object>($"Vaccin Document with ID {id} not found for update.", null, 404));
                }
                return Ok(new ApiResponse<VaccinDocumentDTOResponse>("Update success", updated, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating document: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccinDocument(int id)
        {
            try
            {
                var deleted = await _vaccinDocumentService.DeleteVaccinDocumentAsync(id);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<object>($"Vaccin Document with ID {id} not found for deletion.", null, 404));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting document: {ex.Message}");
            }

        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetVaccinDocumentsPaging(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                var documents = await _vaccinDocumentService.GetAllVaccinDocumentsWithPagingAsync(pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<VaccinDocumentDTOResponse>>("Data retrieved successfully", documents));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving paginated documents: {ex.Message}");
            }
        }
    }
}
