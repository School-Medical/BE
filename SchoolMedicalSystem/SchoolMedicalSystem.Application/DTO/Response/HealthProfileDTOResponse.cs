using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class HealthProfileDTOResponse
    {
        public decimal? WeightIndex { get; set; }

        public decimal? HeightIndex { get; set; }

        public int? VisionIndex { get; set; }

        public int? HearingIndex { get; set; }

        public int? BloodPressureIndex { get; set; }

        public string? AllergyList { get; set; }

        public string? ChronicDisease { get; set; }

        public string? MedicalHistory { get; set; }

        public string? MedicationInUse { get; set; }

        public string? BloodGroup { get; set; }

        public ulong? Gender { get; set; }

        public DateOnly? Birthday { get; set; }

        public DateOnly? UpdateAt { get; set; }

        public string? StudentName { get; set; }
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? ParentPhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public int? UserId { get; set; }
        public int? ClassId { get; set; }
    }
}
