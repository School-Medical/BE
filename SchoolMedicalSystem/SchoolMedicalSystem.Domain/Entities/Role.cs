using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Role
{
    public int role_id { get; set; }

    public string? role_name { get; set; }

    public string? description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
