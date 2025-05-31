using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class HealthCheckConfirmation
{
    public int hc_confirmation_id { get; set; }

    public int? parent_id { get; set; }

    public int? student_id { get; set; }

    public int? health_check_id { get; set; }

    public DateTime? submit_at { get; set; }

    public string? description { get; set; }

    public ulong? status { get; set; }

    public virtual ICollection<HealthCheckDocument> HealthCheckDocuments { get; set; } = new List<HealthCheckDocument>();

    public virtual HealthCheck? health_check { get; set; }

    public virtual User? parent { get; set; }

    public virtual Student? student { get; set; }
}
