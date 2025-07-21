using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class HealthCheckResponse
    {
        public int HealthCheckId { get; set; }
        public string HealthCheckName { get; set; }
        public string HealthCheckDescription { get; set; }
        public string HealthCheckStatus { get; set; } // 1: đang mở, 0: là đang đóng
        public string HealthCheckLocation { get; set; }
        public DateTime HealthCheckStart { get; set; }
        public DateTime HealthCheckEnd { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime RegisteredBy { get; set; }
    }
}
