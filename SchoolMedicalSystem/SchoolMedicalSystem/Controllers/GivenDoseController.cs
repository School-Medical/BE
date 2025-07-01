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
            try
            {
                var data = await _givenDoseService.GetAllAsync();

                foreach (var item in data)
                {
                    var medications = await _medicationService.GetMedicationByGivenDoseId(item.Id);
                    item.ListMedication = medications;
                }

                return Ok(new ApiResponse<List<GivenDoseResponse>>("Lấy danh sách thành công", data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin danh sách GivenDose");
                return BadRequest(new ApiResponse<string>("Lấy danh sách thất bại", [ex.Message], 400));
            }
        }

        //manager, nurse
        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _givenDoseService.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new ApiResponse<string>("Không tìm thấy GivenDose", ["GivenDose không tồn tại"], 404));

                return Ok(new ApiResponse<GivenDoseResponse>("Lấy dữ liệu thành công", result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin GivenDose");
                return BadRequest(new ApiResponse<string>("Lấy thông tin thất bại", [ex.Message], 400));
            }
        }

        //parent
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GivenDoseRequest request)
        {
            try
            {
                var result = await _givenDoseService.AddAsync(request);
                result.ListMedication = await _medicationService.GetMedicationByGivenDoseId(result.Id);
                return Ok(new ApiResponse<GivenDoseResponse>("Thêm thành công", result, 201));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo mới GivenDose");
                return BadRequest(new ApiResponse<string>("Tạo mới thất bại", [ex.Message], 400));
            }
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
            try
            {
                var result = await _givenDoseService.SearchByStudentNameAsync(name);
                return Ok(new ApiResponse<List<GivenDoseResponse>>("Tìm kiếm thành công", result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin GivenDose theo tên học sinh");
                return BadRequest(new ApiResponse<string>("Lấy thông tin thất bại", [ex.Message], 400));
            }
        }
    }
}
