using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class StudentParentDTORequest
    {
        public int StudentParentId { get; set; }

        public int? StudentId { get; set; }

        public int? UserId { get; set; }
    }
}
