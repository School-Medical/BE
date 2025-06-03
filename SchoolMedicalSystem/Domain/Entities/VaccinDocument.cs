using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class VaccinDocument
{
    public int vaccin_document_id { get; set; }

    public int? student_id { get; set; }

    public int? campaign_id { get; set; }

    public int? vaccin_confirm_id { get; set; }

    public int? nurse_id { get; set; }

    public string? pre_condition_vaccin { get; set; }

    public DateTime? injection_time { get; set; }

    public int? duration { get; set; }

    public string? post_condition_vaccin { get; set; }

    public string? adverse_symptoms { get; set; }

    public ulong? status { get; set; }

    public virtual VaccinCampaign? campaign { get; set; }

    public virtual User? nurse { get; set; }

    public virtual Student? student { get; set; }

    public virtual VaccinConfirmation? vaccin_confirm { get; set; }
}
