using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class StudentParent
{
    public int student_parent_id { get; set; }

    public int? student_id { get; set; }

    public int? user_id { get; set; }

    public virtual Student? student { get; set; }

    public virtual User? user { get; set; }
}
