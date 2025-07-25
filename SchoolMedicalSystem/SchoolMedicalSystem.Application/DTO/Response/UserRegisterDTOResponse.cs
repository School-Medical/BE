using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class UserRegisterDTOResponse
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Account { get; set; }

        public string? HashPassword { get; set; }

        public int? RoleId { get; set; }

    }
}
