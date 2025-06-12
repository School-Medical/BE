using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Request
{
    public class HealthProfileDTORequest
    {
        public decimal? weight_index { get; set; }

        public int? hearing_index { get; set; }

        public int? blood_pressure_index { get; set; }

        public string? allergy_list { get; set; }

        public string? chronic_disease { get; set; }

        public string? medical_history { get; set; }

        public string? medication_in_use { get; set; }

        public string? blood_group { get; set; }

        public ulong? gender { get; set; }

        public DateOnly? birthday { get; set; }

        public DateOnly? update_at { get; set; }

        public int? student_id { get; set; }
    }
}
