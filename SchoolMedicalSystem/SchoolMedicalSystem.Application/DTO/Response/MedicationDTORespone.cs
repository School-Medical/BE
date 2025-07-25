using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class MedicationDTORespone
    {
        public int MedicationId { get; set; }
        public string? MedicineName { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
    }
}
