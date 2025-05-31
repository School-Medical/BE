using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Domain.Entities;

public partial class Student
{
    public int student_id { get; set; }

    public string? student_code { get; set; }

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string? parent_phone_number { get; set; }

    public string? image_url { get; set; }

    public int? user_id { get; set; }

    public int? class_id { get; set; }

    public virtual ICollection<GivenDose> GivenDoses { get; set; } = new List<GivenDose>();

    public virtual ICollection<HealthCheckConfirmation> HealthCheckConfirmations { get; set; } = new List<HealthCheckConfirmation>();

    public virtual ICollection<HealthCheckDocument> HealthCheckDocuments { get; set; } = new List<HealthCheckDocument>();

    public virtual ICollection<HealthProfile> HealthProfiles { get; set; } = new List<HealthProfile>();

    public virtual ICollection<MedicalIncident> MedicalIncidents { get; set; } = new List<MedicalIncident>();

    public virtual ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();

    public virtual ICollection<VaccinConfirmation> VaccinConfirmations { get; set; } = new List<VaccinConfirmation>();

    public virtual ICollection<VaccinDocument> VaccinDocuments { get; set; } = new List<VaccinDocument>();

    public virtual Class? _class { get; set; }

    public virtual User? user { get; set; }
}
