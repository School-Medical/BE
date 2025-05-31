﻿using Microsoft.AspNetCore.Builder;

namespace SchoolMedicalSystem.Application.ExceptionHandler
{ 
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
