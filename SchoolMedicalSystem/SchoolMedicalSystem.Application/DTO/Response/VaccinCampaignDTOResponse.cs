using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class VaccinCampaignDTOResponse
    {
        public int VaccinCampaignId { get; set; }

        public string? CampaignName { get; set; }

        public string? CampaignDescription { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string? Location { get; set; }

        public string? VaccinName { get; set; }

        public string? VaccinDescription { get; set; }

        public DateTime? RegisterStart { get; set; }

        public DateTime? RegisterClose { get; set; }

        public ulong? Status { get; set; }

        public string? VaccinNotice { get; set; } = string.Empty;
    }
}
