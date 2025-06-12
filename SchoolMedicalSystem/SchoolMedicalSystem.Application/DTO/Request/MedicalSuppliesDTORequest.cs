using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class MedicalSuppliesDTORequest
    {
        public string? Name { get; set; }
        public string? Effect { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Producer { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ImageUrl { get; set; }
        public ulong? Status { get; set; }
    }
}
