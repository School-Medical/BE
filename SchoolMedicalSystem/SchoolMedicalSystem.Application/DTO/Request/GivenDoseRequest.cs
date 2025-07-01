using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class GivenDoseRequest
    {
        public string? PatientCondition { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Duration { get; set; }
        public string? Message { get; set; }
        public ulong? Status { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
        public int? NurseId { get; set; }
        public List<MedicationRequest> MedicationRequestList { get; set; }
    }
}
