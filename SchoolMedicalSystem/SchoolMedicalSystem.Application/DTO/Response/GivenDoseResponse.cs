using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class GivenDoseResponse
    {
        public int Id { get; set; }

        public string? PatientCondition { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Duration { get; set; }

        public string? Message { get; set; }

        public ulong? Status { get; set; }

        public int? StudentId { get; set; }
        public string? StudentName { get; set; }

        public int? NurseId { get; set; }
        public string? NurseName { get; set; }

        public int? ParentId { get; set; }
        public string? ParentName { get; set; }

        public List<MedicationResponse>? ListMedication { get; set; }
    }
}
