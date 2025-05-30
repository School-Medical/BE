using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Prescription
{
    public int prescription_id { get; set; }

    public string? instruction { get; set; }

    public DateTime? create_at { get; set; }

    public int? medical_incident_id { get; set; }

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();

    public virtual MedicalIncident? medical_incident { get; set; }
}
