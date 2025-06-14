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
        private readonly IJWTService _jwtService;
        public AuthController(IAuthService authService, IJWTService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTORequest request)
        {
            var (isValid, error) = UserValidationHelper.ValidateLogin(request);
            if (!isValid)
                return BadRequest(error);
            var user = await _authService.ValidateUserAsync(request.Account, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid account or password.");
            }   
            user.Token = _jwtService.GenerateJwtToken(user);
            return Ok(new ApiResponse<UserLoginDTOResponse>("Login success fully!",user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTORequest request)
        {
            var (isValid, error) = UserValidationHelper.ValidateRegister(request);
            if (!isValid)
                return BadRequest(error);
            var user = await _authService.CreatedAccountAsync(request);
            if (user == null)
            {
                return BadRequest("Account already exists or registration failed.");
            }
            return Ok(new ApiResponse<UserRegisterDTOResponse>("Registration success fully!", user));
        }
    }
}
