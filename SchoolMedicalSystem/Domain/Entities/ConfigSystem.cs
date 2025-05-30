using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class ConfigSystem
{
    public int config_id { get; set; }

    public string? config_name { get; set; }

    public string? config_value { get; set; }

    public string? config_unit { get; set; }

    public string? description { get; set; }

    public DateTime? update_at { get; set; }
}
