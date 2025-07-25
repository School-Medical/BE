using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class MedicationHistoryDTOResponse
    {
        public int GivenDoseId { get; set; }
        public string? PatientCondition { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Duration { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public int? StudentId { get; set; }
        public int? NurseId { get; set; }
        public List<MedicationDTORespone> Medications { get; set; } = new();
    }
}
