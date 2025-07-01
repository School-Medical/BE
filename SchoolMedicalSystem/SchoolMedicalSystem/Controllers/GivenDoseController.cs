using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GivenDoseController : Controller
    {
        private readonly IGivenDoseService _givenDoseService;
        private readonly IMedicationService _medicationService;
        private readonly ILogger<GivenDoseController> _logger;

        public GivenDoseController(IGivenDoseService givenDoseService, ILogger<GivenDoseController> logger, IMedicationService medicationService)
        {
            _givenDoseService = givenDoseService;
            _medicationService = medicationService;
            _logger = logger;
        }

        //manager, nurse
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _givenDoseService.GetAllAsync();

            foreach (var item in data)
            {
                var medications = await _medicationService.GetMedicationByGivenDoseId(item.Id);
                item.ListMedication = medications;
            }

            return Ok(new ApiResponse<List<GivenDoseResponse>>("Lấy danh sách thành công", data));
        }

        //manager, nurse
        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _givenDoseService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ApiResponse<string>("Không tìm thấy GivenDose", ["GivenDose không tồn tại"], 404));

            return Ok(new ApiResponse<GivenDoseResponse>("Lấy dữ liệu thành công", result));
        }

        //parent
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GivenDoseRequest request)
        {
            var result = await _givenDoseService.AddAsync(request);
            return Ok(new ApiResponse<GivenDoseResponse>("Thêm thành công", result, 201));
        }

        //parent, nurse
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GivenDoseRequest request)
        {
            try
            {
                var result = await _givenDoseService.UpdateAsync(id, request);
                return Ok(new ApiResponse<GivenDoseResponse>("Cập nhật thành công", result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật GivenDose");
                return BadRequest(new ApiResponse<string>("Cập nhật thất bại", [ex.Message], 400));
            }
        }

        //manager, nurse
        [HttpGet("search-by-student-name")]
        public async Task<IActionResult> SearchByStudentName([FromQuery] string name)
        {
            var result = await _givenDoseService.SearchByStudentNameAsync(name);
            return Ok(new ApiResponse<List<GivenDoseResponse>>("Tìm kiếm thành công", result));
        }
    }
}
