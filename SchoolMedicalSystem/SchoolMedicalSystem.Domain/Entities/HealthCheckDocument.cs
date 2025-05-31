using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class HealthCheckDocument
{
    public int hc_document_id { get; set; }

    public DateTime? check_at { get; set; }

    public decimal? weight_index { get; set; }

    public decimal? height_index { get; set; }

    public int? vision_index { get; set; }

    public int? hearing_index { get; set; }

    public int? blood_pressure_index { get; set; }

    public string? description { get; set; }

    public ulong? status { get; set; }

    public int? student_id { get; set; }

    public int? health_check_id { get; set; }

    public int? health_check_confirm_id { get; set; }

    public int? nurse_id { get; set; }

    public virtual HealthCheck? health_check { get; set; }

    public virtual HealthCheckConfirmation? health_check_confirm { get; set; }

    public virtual User? nurse { get; set; }

    public virtual Student? student { get; set; }
}
