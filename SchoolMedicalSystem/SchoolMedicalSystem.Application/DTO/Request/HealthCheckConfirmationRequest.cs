using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class HealthCheckConfirmationRequest
    {
        [Required(ErrorMessage = "Student ID is required")]
        public int student_id { get; set; }

        [Required(ErrorMessage = "Health Check ID is required")]
        public int health_check_id { get; set; }

        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? description { get; set; }

        [Range(0, 2, ErrorMessage = "Status must be 0 (Not Confirmed), 1 (Confirmed), or 2 (Rejected)")]
        public int status { get; set; } = 0; // Default: Not confirmed
    }
}
