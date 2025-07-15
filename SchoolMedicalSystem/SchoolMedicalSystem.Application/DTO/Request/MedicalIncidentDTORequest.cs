using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class MedicalIncidentDTORequest
    {
        public string? Type { get; set; }
        public string? Symptom { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public int? SeverityLevel { get; set; }
        public ulong? FollowUp { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? StudentId { get; set; }
        public int? NurseId { get; set; }
        public List<AddPrescriptionDTORequest> Prescriptions { get; set; } = new List<AddPrescriptionDTORequest>();
    }
}
