using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class PrescriptionMedicineDTORespone
    {
        public int PrescriptionMedicineId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? Quantity { get; set; }
    }
}
