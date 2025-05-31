using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Infrastructure.Data;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SchoolMedicalDbContext _context;

        public TestController(SchoolMedicalDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Thử truy vấn đơn giản
                var canConnect = await _context.Database.CanConnectAsync();
                if (canConnect)
                    return Ok("Kết nối MySQL thành công!");
                else
                    return StatusCode(500, "Không thể kết nối MySQL");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi kết nối: {ex.Message}");
            }
        }
    }
}
