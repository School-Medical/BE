using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class VaccinCampaign
{
    public int vaccin_campaign_id { get; set; }

    public string? campaign_name { get; set; }

    public string? campaign_description { get; set; }

    public DateTime? start_at { get; set; }

    public DateTime? end_at { get; set; }

    public string? location { get; set; }

    public string? vaccin_name { get; set; }

    public string? vaccin_description { get; set; }

    public DateTime? register_start { get; set; }

    public DateTime? register_close { get; set; }

    public ulong? status { get; set; }

    public string? vaccin_notice { get; set; }

    public virtual ICollection<VaccinConfirmation> VaccinConfirmations { get; set; } = new List<VaccinConfirmation>();

    public virtual ICollection<VaccinDocument> VaccinDocuments { get; set; } = new List<VaccinDocument>();
}
