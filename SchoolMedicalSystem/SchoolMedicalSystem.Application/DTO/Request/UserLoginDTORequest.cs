using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class UserLoginDTORequest
    {
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
