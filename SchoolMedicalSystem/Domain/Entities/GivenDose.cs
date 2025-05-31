using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class GivenDose
{
    public int given_dose_id { get; set; }

    public string? patient_condition { get; set; }

    public DateTime? create_at { get; set; }

    public int? duration { get; set; }

    public string? message { get; set; }

    public ulong? status { get; set; }

    public int? student_id { get; set; }

    public int? parent_id { get; set; }

    public int? nurse_id { get; set; }

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    public virtual User? nurse { get; set; }

    public virtual User? parent { get; set; }

    public virtual Student? student { get; set; }
}
