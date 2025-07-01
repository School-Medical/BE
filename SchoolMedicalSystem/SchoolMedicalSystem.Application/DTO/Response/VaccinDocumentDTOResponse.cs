using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class VaccinDocumentDTOResponse
    {
        public int VaccinDocumentId { get; set; }

        public int? StudentId { get; set; }
        public string? StudentName { get; set; }

        public int? CampaignId { get; set; }

        public string? CampaignName { get; set; }

        public int? VaccinConfirmId { get; set; }

        public string? VaccinName { get; set; }

        public DateTime? SubmitAt { get; set; }

        public string? Message { get; set; }

        public ulong? VaccinConfirmStatus { get; set; }

        public int? NurseId { get; set; }

        public string? NurseName { get; set; }

        public string? PreConditionVaccin { get; set; }

        public DateTime? InjectionTime { get; set; }

        public int? Duration { get; set; }

        public string? PostConditionVaccin { get; set; }

        public string? AdverseSymptoms { get; set; }

        public ulong? Status { get; set; }

    }
}
