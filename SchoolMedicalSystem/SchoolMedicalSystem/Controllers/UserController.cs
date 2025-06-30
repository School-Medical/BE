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
    public class UserController : ControllerBase
    {
        // This is a placeholder for the actual implementation of user service
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {
                if (pageSize <= 0 || pageNumber <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid pagination parameters",
                        new List<string> { "PageSize and PageNumber must be greater than 0" }, 400));
                }

                if (pageSize > 100)
                {
                    return BadRequest(new ApiResponse<object>("PageSize exceeds maximum limit",
                        new List<string> { "PageSize cannot be greater than 100" }, 400));
                }

                // This is a placeholder for the actual implementation
                var users = await _userService.GetAllAsync(pageSize, pageNumber);
                return Ok(new ApiResponse<PaginatedResponse<UserDTOResponse>>("Users are retrieved successfully", users, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                if(userId <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid user ID", 
                        new List<string> { "User ID must be greater than 0" }, 400));
                }

                var result = await _userService.GetByIdAsync(userId);
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>("User not found",
                        new List<string> { "No user found with the provided ID" }, 404));
                }

                return Ok(new ApiResponse<UserDTOResponse>("User retrieved successfully", result, 200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
            
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDTORequest userDto)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new ApiResponse<object>("Invalid user ID",
                        new List<string> { "User ID must be greater than 0" }, 400));
                }

                var updatedUser = await _userService.UpdateAsync(userId, userDto);
                return Ok(new ApiResponse<UserDTOResponse>("User updated successfully", updatedUser, 200));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>("Invalid input", new List<string> { ex.Message }, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Internal server error", new List<string> { ex.Message }, 500));
            }
        }
    }
}
