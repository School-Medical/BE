using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Class
{
    public int class_id { get; set; }

    public string? class_name { get; set; }

    public int? class_size { get; set; }

    public DateOnly? start_at { get; set; }

    public DateOnly? end_at { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
