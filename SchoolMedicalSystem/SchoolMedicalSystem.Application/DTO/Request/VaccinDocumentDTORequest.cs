using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class VaccinDocumentDTORequest
    {
        public int VaccinDocumentId { get; set; }

        public int? StudentId { get; set; }

        public int? CampaignId { get; set; }

        public int? VaccinConfirmId { get; set; }

        public int? NurseId { get; set; }

        public string? PreConditionVaccin { get; set; }

        public DateTime? InjectionTime { get; set; }

        public int? Duration { get; set; }

        public string? PostConditionVaccin { get; set; }

        public string? AdverseSymptoms { get; set; }

        public ulong? Status { get; set; }

    }
}
