using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class VaccinConfirmationDTOResponse
    {
        public int VaccinConfirmationId { get; set; }

        public DateTime? SubmitAt { get; set; }

        public string? Message { get; set; }

        public ulong? Status { get; set; }

        public int? ParentId { get; set; }
        public string? ParentName { get; set; } = string.Empty;
        public int? StudentId { get; set; }
        public string? StudentName { get; set; } = string.Empty;

        public int? CampaignId { get; set; }
        public string? CampaignName { get; set; } = string.Empty;
        public string? VaccinName { get; set; } = string.Empty;

    }
}
