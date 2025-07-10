using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class AddPrescriptionMedicineDTORequest
    {
        public int? PescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public int? Quantity { get; set; }
    }
}
