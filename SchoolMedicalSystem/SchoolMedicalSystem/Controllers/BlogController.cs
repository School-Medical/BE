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
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(new ApiResponse<IEnumerable<BlogDTOResponse>>("Blog created successfully", blogs, 200));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<BlogDTOResponse>("Blog retrieved successfully", blog, 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromForm] BlogDTORequest blog)
        {
            if (blog == null)
            {
                return BadRequest("Blog cannot be null");
            }
            var createdBlog = await _blogService.CreateBlogAsync(blog);
            return Ok( new  ApiResponse<BlogDTOResponse>( "Blog created successfully",createdBlog,200));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromForm] BlogDTORequest blog)
        {          

            var updated = await _blogService.UpdateBlogAsync(id, blog);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent(); // Có nên trả về 204 No Content khi cập nhật thành công không?
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var deleted = await _blogService.DeleteBlogAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetBlogsWithPaging(int pageSize, int pageNumber)
        {
            var blogs = await _blogService.GetAllBlogsWithPagingAsync(pageSize, pageNumber);
            return Ok(new ApiResponse<PaginatedResponse<BlogDTOResponse>>("Blogs retrieved successfully", blogs, 200));
        }
    }
}
