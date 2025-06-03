using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Batch
{
    public int batch_id { get; set; }

    public DateTime? import_date { get; set; }

    public int? user_id { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual User? user { get; set; }
}
