using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class HealthCheckConfirmationResponse
    {
        public int HealthCheckConfirmationId { get; set; }

        public int? ParentId { get; set; }

        public int? StudentId { get; set; }

        public int? HealthCheckId { get; set; }

        public DateTime? SubmitAt { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
