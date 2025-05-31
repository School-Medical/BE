using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class MedicalIncident
{
    public int medical_incident_id { get; set; }

    public string? type { get; set; }

    public string? symptom { get; set; }

    public string? diagnosis { get; set; }

    public string? treatment { get; set; }

    public int? severity_level { get; set; }

    public ulong? follow_up { get; set; }

    public string? message { get; set; }

    public DateTime? create_at { get; set; }

    public int? student_id { get; set; }

    public int? nurse_id { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual User? nurse { get; set; }

    public virtual Student? student { get; set; }
}
