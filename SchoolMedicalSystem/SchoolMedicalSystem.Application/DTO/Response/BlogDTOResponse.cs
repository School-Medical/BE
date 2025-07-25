using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class BlogDTOResponse
    {
        public int BlogId { get; set; }

        public int? UserId { get; set; }

        public string? ImageUrl { get; set; }

        public string? Content { get; set; }

        public string? Title { get; set; }

        public string? Type { get; set; }

        public DateOnly? UpdatedAt { get; set; }
    }
}
