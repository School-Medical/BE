using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class User
{
    public int user_id { get; set; }

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string? address { get; set; }

    public string? phone_number { get; set; }

    public string? account { get; set; }

    public string? hash_password { get; set; }

    public int? role_id { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<GivenDose> GivenDosenurses { get; set; } = new List<GivenDose>();

    public virtual ICollection<GivenDose> GivenDoseparents { get; set; } = new List<GivenDose>();

    public virtual ICollection<HealthCheckConfirmation> HealthCheckConfirmations { get; set; } = new List<HealthCheckConfirmation>();

    public virtual ICollection<HealthCheckDocument> HealthCheckDocuments { get; set; } = new List<HealthCheckDocument>();

    public virtual ICollection<MedicalIncident> MedicalIncidents { get; set; } = new List<MedicalIncident>();

    public virtual ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<VaccinConfirmation> VaccinConfirmations { get; set; } = new List<VaccinConfirmation>();

    public virtual ICollection<VaccinDocument> VaccinDocuments { get; set; } = new List<VaccinDocument>();

    public virtual Role? role { get; set; }
}
