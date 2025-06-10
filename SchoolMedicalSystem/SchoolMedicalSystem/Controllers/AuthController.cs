using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Models;
using static SchoolMedicalSystem.Application.DTO.Response.UserLoginDTOResponse;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTORequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var user = await _authService.ValidateUserAsync(request.Account, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid account or password.");
            }   
            user.Token = _authService.GenerateJwtToken(user);
            return Ok(new ApiResponse<UserLoginDTOResponse>("Login success fully!",user));
        }
    }
}
