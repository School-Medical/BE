using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Medicine
{
    public int medicine_id { get; set; }

    public string? medicine_name { get; set; }

    public string? medicine_effect { get; set; }

    public string? medicine_usage { get; set; }

    public string? medicine_type { get; set; }

    public string? category { get; set; }

    public string? producer { get; set; }

    public int? quantity { get; set; }

    public string? unit { get; set; }

    public DateTime? expiry_date { get; set; }

    public string? image_url { get; set; }

    public ulong? status { get; set; }

    public int? batch_id { get; set; }

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();

    public virtual Batch? batch { get; set; }
}
