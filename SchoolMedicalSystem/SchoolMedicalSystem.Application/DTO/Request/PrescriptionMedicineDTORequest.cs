using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class PrescriptionMedicineDTORequest
    {
        public int PrescriptionMedicineId { get; set; }
        public int? PescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public int? Quantity { get; set; }
    }
}
