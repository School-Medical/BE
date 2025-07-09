using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class AddPrescriptionDTORequest
    {

        public string? Instruction { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? MedicalIncidentId { get; set; }
        public List<AddPrescriptionMedicineDTORequest>? PrescriptionMedicines { get; set; }
    }
}
