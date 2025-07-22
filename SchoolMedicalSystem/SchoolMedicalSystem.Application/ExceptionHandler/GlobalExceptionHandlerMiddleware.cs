using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Response;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;


namespace SchoolMedicalSystem.Application.ExceptionHandler
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Middleware entry point. Invokes the next middleware in the pipeline and handles any exceptions thrown.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   ex,
                   "Lỗi không mong muốn tại {Path}. Error: {Error}",
                   context.Request.Path,
                   ex.Message
               );
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles exceptions by setting the appropriate status code and response content.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        /// <param name="exception">The exception that was thrown.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var (statusCode, response) = exception switch
            {
                ValidationException ex => (
                    StatusCodes.Status400BadRequest,
                    ErrorResponse.Create(ex.Message, "VALIDATION_ERROR")
                ),

                NotFoundException ex => (
                    StatusCodes.Status404NotFound,
                    ErrorResponse.Create(ex.Message, "NOT_FOUND")
                ),

                //DbUpdateException ex => HandleDbUpdateException(ex),

                //UnauthorizedAccessException ex => (
                //    StatusCodes.Status401Unauthorized,
                //    ErrorResponse.Create(ex.Message, "UNAUTHORIZED")
                //),

                ArgumentNullException ex => (
                    StatusCodes.Status400BadRequest,
                    ErrorResponse.Create(ex.Message, "ARGUMENT_NULL")
                ),

                InvalidOperationException ex => (
                    StatusCodes.Status400BadRequest,
                    ErrorResponse.Create(ex.Message, "INVALID_OPERATION")
                ),

                BadRequestException ex => (
                    StatusCodes.Status400BadRequest,
                    ErrorResponse.Create(ex.Message, "BAD_REQUEST")
                ),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    CreateInternalServerError(exception)
                )
            };

            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }

        /// <summary>
        /// Handles database update exceptions by setting the appropriate status code and response content.
        /// </summary>
        /// <param name="ex">The DbUpdateException that was thrown.</param>
        /// <returns>A tuple containing the status code and error response.</returns>
        //private static (int StatusCode, ErrorResponse Response) HandleDbUpdateException(DbUpdateException ex)
        //{
        //    var (message, errorCode) = ex.InnerException switch
        //    {
        //        SqlException sqlEx => sqlEx.Number switch
        //        {
        //            547 => ("Không thể xóa dữ liệu do ràng buộc khóa ngoại", "DB_FOREIGN_KEY_ERROR"),
        //            2601 or 2627 => ("Dữ liệu bị trùng lặp", "DB_DUPLICATE_ERROR"),
        //            _ => ("Lỗi thao tác với cơ sở dữ liệu", "DB_ERROR")
        //        },
        //        _ => ("Lỗi thao tác với cơ sở dữ liệu", "DB_ERROR")
        //    };

        //    return (StatusCodes.Status400BadRequest, ErrorResponse.Create(message, errorCode));
        //}

        /// <summary>
        /// Creates an internal server error response.
        /// </summary>
        /// <param name="ex">The exception that was thrown.</param>
        /// <returns>An ErrorResponse representing the internal server error.</returns>
        private ErrorResponse CreateInternalServerError(Exception ex)
        {
            var message = _env.IsDevelopment()
                ? $"Internal Server Error: {ex.Message}"
                : $"Đã xảy ra lỗi trong quá trình xử lý + {ex.Message}";

            return ErrorResponse.Create(message, "INTERNAL_ERROR");
        }
    }
}
