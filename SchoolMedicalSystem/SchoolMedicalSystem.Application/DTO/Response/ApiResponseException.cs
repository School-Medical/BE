using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class ApiResponseException
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
