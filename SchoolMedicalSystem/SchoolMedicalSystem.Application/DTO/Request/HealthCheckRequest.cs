using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class HealthCheckRequest
    {
        public string HealthCheckName { get; set; }
        public string HealthCheckDescription { get; set; }
        public string HealthCheckLocation { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? RegisterStart {  get; set; }
        public DateTime? RegisterEnd { get; set; }
    }
}
