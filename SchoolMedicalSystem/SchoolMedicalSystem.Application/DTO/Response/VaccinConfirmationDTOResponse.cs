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

        public int? StudentId { get; set; }

        public int? CampaignId { get; set; }

    }
}
