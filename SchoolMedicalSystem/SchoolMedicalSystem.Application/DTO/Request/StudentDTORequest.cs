using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class StudentDTORequest
    {
        public string? StudentCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? ClassId { get; set; }
    }
}
