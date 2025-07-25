using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class HealthCheckDocumentResponse
    {
        public int HealthCheckDocumentId { get; set; }
        public DateTime? CheckAt { get; set; }
        public decimal? WeightIndex { get; set; }
        public decimal? HeightIndex { get; set; }
        public int? VisionIndex { get; set; }
        public int? HearingIndex { get; set; }
        public int? BloodPressureIndex { get; set; }
        public string? Description { get; set; }
        public ulong? Status { get; set; }
        public int? StudentId { get; set; }
        public int? HealthCheckId { get; set; }
        public int? HealthCheckConfirmId { get; set; }
        public int? NurseId { get; set; }
    }
}
