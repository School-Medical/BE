using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Medication
{
    public int medication_id { get; set; }

    public string? medicine_name { get; set; }

    public int? quantity { get; set; }

    public string? unit { get; set; }

    public string? type { get; set; }

    public string? message { get; set; }

    public int? given_dose_id { get; set; }

    public virtual GivenDose? given_dose { get; set; }
}
