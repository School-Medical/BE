using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class PrescriptionMedicine
{
    public int prescription_medicine_id { get; set; }

    public int? prescription_id { get; set; }

    public int? medicine_id { get; set; }

    public int? quantity { get; set; }

    public virtual Medicine? medicine { get; set; }

    public virtual Prescription? prescription { get; set; }
}
