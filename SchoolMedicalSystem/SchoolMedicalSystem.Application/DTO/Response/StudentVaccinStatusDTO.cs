using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class StudentVaccinStatusDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public int ConfirmId { get; set; }
        public DateTime? ConfirmSubmitAt { get; set; }
        public string? ConfirmMessage { get; set; }
        public ulong? StatusConfirm { get; set; }
    }
}
