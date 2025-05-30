using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class HealthCheck
{
    public int health_check_id { get; set; }

    public string? hc_name { get; set; }

    public string? hc_description { get; set; }

    public string? location { get; set; }

    public DateTime? start_at { get; set; }

    public DateTime? end_at { get; set; }

    public DateTime? register_start { get; set; }

    public DateTime? register_close { get; set; }

    public ulong? status { get; set; }

    public virtual ICollection<HealthCheckConfirmation> HealthCheckConfirmations { get; set; } = new List<HealthCheckConfirmation>();

    public virtual ICollection<HealthCheckDocument> HealthCheckDocuments { get; set; } = new List<HealthCheckDocument>();
}
