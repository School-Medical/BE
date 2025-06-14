using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class UserLoginDTOResponse
    {
            public int UserId { get; set; }
            public string Account { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string Token { get; set; } = null!;
            public string? RoleName { get; set; }

    }
}
