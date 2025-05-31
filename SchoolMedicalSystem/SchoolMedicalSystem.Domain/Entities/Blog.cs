using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Blog
{
    public int blog_id { get; set; }

    public int? user_id { get; set; }

    public string? image_url { get; set; }

    public string? content { get; set; }

    public string? type { get; set; }

    public DateOnly? update_at { get; set; }

    public virtual User? user { get; set; }
}
