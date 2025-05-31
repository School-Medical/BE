using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class VaccinConfirmation
{
    public int vaccin_confirmation_id { get; set; }

    public DateTime? submit_at { get; set; }

    public string? message { get; set; }

    public ulong? status { get; set; }

    public int? parent_id { get; set; }

    public int? student_id { get; set; }

    public int? campaign_id { get; set; }

    public virtual ICollection<VaccinDocument> VaccinDocuments { get; set; } = new List<VaccinDocument>();

    public virtual VaccinCampaign? campaign { get; set; }

    public virtual User? parent { get; set; }

    public virtual Student? student { get; set; }
}
