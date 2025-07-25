using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class BlogDTORequest
    {
        public int? UserId { get; set; }

        public IFormFile? ImageUrl { get; set; } // Changed to IFormFile for image upload

        public string? Content { get; set; }

        public string Title { get; set; }

        public string? Type { get; set; }

        public DateOnly? UpdatedAt { get; set; } = new DateOnly();
    }
}
