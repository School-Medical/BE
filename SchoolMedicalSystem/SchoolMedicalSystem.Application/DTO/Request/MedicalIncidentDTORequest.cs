using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class MedicalIncidentDTORequest
    {
        public string? type { get; set; }

        public string? symptom { get; set; }

        public string? diagnosis { get; set; }

        public string? treatment { get; set; }

        public int? severity_level { get; set; }

        public ulong? follow_up { get; set; }

        public string? message { get; set; }

        public DateTime? create_at { get; set; }

        public int? student_id { get; set; }

        public int? nurse_id { get; set; }
    }
}
