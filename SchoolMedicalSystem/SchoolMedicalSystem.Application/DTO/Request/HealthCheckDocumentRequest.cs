using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class HealthCheckDocumentRequest
    {
        public DateTime? CheckAt { get; set; }

        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 kg")]
        public decimal? WeightIndex { get; set; }

        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 cm")]
        public decimal? HeightIndex { get; set; }

        [Range(0, 100, ErrorMessage = "Vision index must be between 0 and 100")]
        public int? VisionIndex { get; set; }

        [Range(0, 100, ErrorMessage = "Hearing index must be between 0 and 100")]
        public int? HearingIndex { get; set; }

        [Range(0, 300, ErrorMessage = "Blood pressure index must be between 0 and 300")]
        public int? BloodPressureIndex { get; set; }

        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Range(0, 1, ErrorMessage = "Status must be 0 or 1")]
        public ulong? Status { get; set; } = 1;

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int HealthCheckId { get; set; }

        [Required]
        public int HealthCheckConfirmId { get; set; }

        [Required]
        public int NurseId { get; set; }
    }
}
