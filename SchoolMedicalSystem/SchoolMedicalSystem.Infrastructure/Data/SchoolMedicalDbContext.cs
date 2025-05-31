using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Infrastructure.Data;

public partial class SchoolMedicalDbContext : DbContext
{
    public SchoolMedicalDbContext(DbContextOptions<SchoolMedicalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ConfigSystem> ConfigSystems { get; set; }

    public virtual DbSet<GivenDose> GivenDoses { get; set; }

    public virtual DbSet<HealthCheck> HealthChecks { get; set; }

    public virtual DbSet<HealthCheckConfirmation> HealthCheckConfirmations { get; set; }

    public virtual DbSet<HealthCheckDocument> HealthCheckDocuments { get; set; }

    public virtual DbSet<HealthProfile> HealthProfiles { get; set; }

    public virtual DbSet<MedicalIncident> MedicalIncidents { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentParent> StudentParents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccinCampaign> VaccinCampaigns { get; set; }

    public virtual DbSet<VaccinConfirmation> VaccinConfirmations { get; set; }

    public virtual DbSet<VaccinDocument> VaccinDocuments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.batch_id).HasName("PRIMARY");

            entity.HasIndex(e => e.user_id, "user_id");

            entity.Property(e => e.import_date).HasColumnType("datetime");

            entity.HasOne(d => d.user).WithMany(p => p.Batches)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("Batches_ibfk_1");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.blog_id).HasName("PRIMARY");

            entity.HasIndex(e => e.user_id, "user_id");

            entity.Property(e => e.content).HasColumnType("text");
            entity.Property(e => e.image_url).HasColumnType("text");
            entity.Property(e => e.type).HasMaxLength(50);

            entity.HasOne(d => d.user).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("Blogs_ibfk_1");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.class_id).HasName("PRIMARY");

            entity.HasIndex(e => e.class_name, "class_name").IsUnique();

            entity.Property(e => e.class_name).HasMaxLength(30);
        });

        modelBuilder.Entity<ConfigSystem>(entity =>
        {
            entity.HasKey(e => e.config_id).HasName("PRIMARY");

            entity.Property(e => e.config_name).HasMaxLength(100);
            entity.Property(e => e.config_unit).HasMaxLength(50);
            entity.Property(e => e.config_value).HasMaxLength(100);
            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.update_at).HasColumnType("datetime");
        });

        modelBuilder.Entity<GivenDose>(entity =>
        {
            entity.HasKey(e => e.given_dose_id).HasName("PRIMARY");

            entity.HasIndex(e => e.nurse_id, "nurse_id");

            entity.HasIndex(e => e.parent_id, "parent_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.create_at).HasColumnType("datetime");
            entity.Property(e => e.message).HasColumnType("text");
            entity.Property(e => e.patient_condition).HasColumnType("text");
            entity.Property(e => e.status).HasColumnType("bit(1)");

            entity.HasOne(d => d.nurse).WithMany(p => p.GivenDosenurses)
                .HasForeignKey(d => d.nurse_id)
                .HasConstraintName("GivenDoses_ibfk_3");

            entity.HasOne(d => d.parent).WithMany(p => p.GivenDoseparents)
                .HasForeignKey(d => d.parent_id)
                .HasConstraintName("GivenDoses_ibfk_2");

            entity.HasOne(d => d.student).WithMany(p => p.GivenDoses)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("GivenDoses_ibfk_1");
        });

        modelBuilder.Entity<HealthCheck>(entity =>
        {
            entity.HasKey(e => e.health_check_id).HasName("PRIMARY");

            entity.Property(e => e.end_at).HasColumnType("datetime");
            entity.Property(e => e.hc_description).HasColumnType("text");
            entity.Property(e => e.hc_name).HasMaxLength(100);
            entity.Property(e => e.location).HasMaxLength(100);
            entity.Property(e => e.register_close).HasColumnType("datetime");
            entity.Property(e => e.register_start).HasColumnType("datetime");
            entity.Property(e => e.start_at).HasColumnType("datetime");
            entity.Property(e => e.status).HasColumnType("bit(1)");
        });

        modelBuilder.Entity<HealthCheckConfirmation>(entity =>
        {
            entity.HasKey(e => e.hc_confirmation_id).HasName("PRIMARY");

            entity.HasIndex(e => e.health_check_id, "health_check_id");

            entity.HasIndex(e => e.parent_id, "parent_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.status).HasColumnType("bit(1)");
            entity.Property(e => e.submit_at).HasColumnType("datetime");

            entity.HasOne(d => d.health_check).WithMany(p => p.HealthCheckConfirmations)
                .HasForeignKey(d => d.health_check_id)
                .HasConstraintName("HealthCheckConfirmations_ibfk_3");

            entity.HasOne(d => d.parent).WithMany(p => p.HealthCheckConfirmations)
                .HasForeignKey(d => d.parent_id)
                .HasConstraintName("HealthCheckConfirmations_ibfk_1");

            entity.HasOne(d => d.student).WithMany(p => p.HealthCheckConfirmations)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("HealthCheckConfirmations_ibfk_2");
        });

        modelBuilder.Entity<HealthCheckDocument>(entity =>
        {
            entity.HasKey(e => e.hc_document_id).HasName("PRIMARY");

            entity.HasIndex(e => e.health_check_confirm_id, "health_check_confirm_id");

            entity.HasIndex(e => e.health_check_id, "health_check_id");

            entity.HasIndex(e => e.nurse_id, "nurse_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.check_at).HasColumnType("datetime");
            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.height_index).HasPrecision(5, 2);
            entity.Property(e => e.status).HasColumnType("bit(1)");
            entity.Property(e => e.weight_index).HasPrecision(5, 2);

            entity.HasOne(d => d.health_check_confirm).WithMany(p => p.HealthCheckDocuments)
                .HasForeignKey(d => d.health_check_confirm_id)
                .HasConstraintName("HealthCheckDocuments_ibfk_3");

            entity.HasOne(d => d.health_check).WithMany(p => p.HealthCheckDocuments)
                .HasForeignKey(d => d.health_check_id)
                .HasConstraintName("HealthCheckDocuments_ibfk_2");

            entity.HasOne(d => d.nurse).WithMany(p => p.HealthCheckDocuments)
                .HasForeignKey(d => d.nurse_id)
                .HasConstraintName("HealthCheckDocuments_ibfk_4");

            entity.HasOne(d => d.student).WithMany(p => p.HealthCheckDocuments)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("HealthCheckDocuments_ibfk_1");
        });

        modelBuilder.Entity<HealthProfile>(entity =>
        {
            entity.HasKey(e => e.health_profile_id).HasName("PRIMARY");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.allergy_list).HasColumnType("text");
            entity.Property(e => e.blood_group).HasColumnType("enum('A','B','AB','O')");
            entity.Property(e => e.chronic_disease).HasColumnType("text");
            entity.Property(e => e.gender).HasColumnType("bit(1)");
            entity.Property(e => e.height_index).HasPrecision(5, 2);
            entity.Property(e => e.medical_history).HasColumnType("text");
            entity.Property(e => e.medication_in_use).HasColumnType("text");
            entity.Property(e => e.weight_index).HasPrecision(5, 2);

            entity.HasOne(d => d.student).WithMany(p => p.HealthProfiles)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("HealthProfiles_ibfk_1");
        });

        modelBuilder.Entity<MedicalIncident>(entity =>
        {
            entity.HasKey(e => e.medical_incident_id).HasName("PRIMARY");

            entity.HasIndex(e => e.nurse_id, "nurse_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.create_at).HasColumnType("datetime");
            entity.Property(e => e.diagnosis).HasColumnType("text");
            entity.Property(e => e.follow_up).HasColumnType("bit(1)");
            entity.Property(e => e.message).HasColumnType("text");
            entity.Property(e => e.symptom).HasColumnType("text");
            entity.Property(e => e.treatment).HasColumnType("text");
            entity.Property(e => e.type).HasMaxLength(50);

            entity.HasOne(d => d.nurse).WithMany(p => p.MedicalIncidents)
                .HasForeignKey(d => d.nurse_id)
                .HasConstraintName("MedicalIncidents_ibfk_2");

            entity.HasOne(d => d.student).WithMany(p => p.MedicalIncidents)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("MedicalIncidents_ibfk_1");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.medication_id).HasName("PRIMARY");

            entity.HasIndex(e => e.given_dose_id, "given_dose_id");

            entity.Property(e => e.medicine_name).HasMaxLength(100);
            entity.Property(e => e.message).HasColumnType("text");
            entity.Property(e => e.type).HasMaxLength(50);
            entity.Property(e => e.unit).HasMaxLength(20);

            entity.HasOne(d => d.given_dose).WithMany(p => p.Medications)
                .HasForeignKey(d => d.given_dose_id)
                .HasConstraintName("Medications_ibfk_1");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.medicine_id).HasName("PRIMARY");

            entity.HasIndex(e => e.batch_id, "batch_id");

            entity.Property(e => e.category).HasColumnType("enum('medicine','supply')");
            entity.Property(e => e.expiry_date).HasColumnType("datetime");
            entity.Property(e => e.image_url).HasColumnType("text");
            entity.Property(e => e.medicine_effect).HasColumnType("text");
            entity.Property(e => e.medicine_name).HasMaxLength(100);
            entity.Property(e => e.medicine_type).HasMaxLength(50);
            entity.Property(e => e.medicine_usage).HasColumnType("text");
            entity.Property(e => e.producer).HasMaxLength(100);
            entity.Property(e => e.status).HasColumnType("bit(1)");
            entity.Property(e => e.unit).HasMaxLength(20);

            entity.HasOne(d => d.batch).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.batch_id)
                .HasConstraintName("Medicines_ibfk_1");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.prescription_id).HasName("PRIMARY");

            entity.HasIndex(e => e.medical_incident_id, "medical_incident_id");

            entity.Property(e => e.create_at).HasColumnType("datetime");
            entity.Property(e => e.instruction).HasColumnType("text");

            entity.HasOne(d => d.medical_incident).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.medical_incident_id)
                .HasConstraintName("Prescriptions_ibfk_1");
        });

        modelBuilder.Entity<PrescriptionMedicine>(entity =>
        {
            entity.HasKey(e => e.prescription_medicine_id).HasName("PRIMARY");

            entity.HasIndex(e => e.medicine_id, "medicine_id");

            entity.HasIndex(e => e.prescription_id, "prescription_id");

            entity.HasOne(d => d.medicine).WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(d => d.medicine_id)
                .HasConstraintName("PrescriptionMedicines_ibfk_2");

            entity.HasOne(d => d.prescription).WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(d => d.prescription_id)
                .HasConstraintName("PrescriptionMedicines_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.role_id).HasName("PRIMARY");

            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.role_name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.student_id).HasName("PRIMARY");

            entity.HasIndex(e => e.class_id, "class_id");

            entity.HasIndex(e => e.student_code, "student_code").IsUnique();

            entity.HasIndex(e => e.user_id, "user_id");

            entity.Property(e => e.first_name).HasMaxLength(50);
            entity.Property(e => e.image_url).HasColumnType("text");
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.parent_phone_number).HasMaxLength(10);
            entity.Property(e => e.student_code).HasMaxLength(20);

            entity.HasOne(d => d._class).WithMany(p => p.Students)
                .HasForeignKey(d => d.class_id)
                .HasConstraintName("Students_ibfk_2");

            entity.HasOne(d => d.user).WithMany(p => p.Students)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("Students_ibfk_1");
        });

        modelBuilder.Entity<StudentParent>(entity =>
        {
            entity.HasKey(e => e.student_parent_id).HasName("PRIMARY");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.HasIndex(e => e.user_id, "user_id");

            entity.HasOne(d => d.student).WithMany(p => p.StudentParents)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("StudentParents_ibfk_1");

            entity.HasOne(d => d.user).WithMany(p => p.StudentParents)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("StudentParents_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("PRIMARY");

            entity.HasIndex(e => e.role_id, "role_id");

            entity.Property(e => e.account).HasMaxLength(30);
            entity.Property(e => e.address).HasColumnType("text");
            entity.Property(e => e.first_name).HasMaxLength(50);
            entity.Property(e => e.hash_password).HasMaxLength(255);
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.phone_number).HasMaxLength(10);

            entity.HasOne(d => d.role).WithMany(p => p.Users)
                .HasForeignKey(d => d.role_id)
                .HasConstraintName("Users_ibfk_1");
        });

        modelBuilder.Entity<VaccinCampaign>(entity =>
        {
            entity.HasKey(e => e.vaccin_campaign_id).HasName("PRIMARY");

            entity.Property(e => e.campaign_description).HasColumnType("text");
            entity.Property(e => e.campaign_name).HasMaxLength(100);
            entity.Property(e => e.end_at).HasColumnType("datetime");
            entity.Property(e => e.location).HasMaxLength(100);
            entity.Property(e => e.register_close).HasColumnType("datetime");
            entity.Property(e => e.register_start).HasColumnType("datetime");
            entity.Property(e => e.start_at).HasColumnType("datetime");
            entity.Property(e => e.status).HasColumnType("bit(1)");
            entity.Property(e => e.vaccin_description).HasColumnType("text");
            entity.Property(e => e.vaccin_name).HasMaxLength(100);
            entity.Property(e => e.vaccin_notice).HasMaxLength(50);
        });

        modelBuilder.Entity<VaccinConfirmation>(entity =>
        {
            entity.HasKey(e => e.vaccin_confirmation_id).HasName("PRIMARY");

            entity.HasIndex(e => e.campaign_id, "campaign_id");

            entity.HasIndex(e => e.parent_id, "parent_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.Property(e => e.message).HasColumnType("text");
            entity.Property(e => e.status).HasColumnType("bit(1)");
            entity.Property(e => e.submit_at).HasColumnType("datetime");

            entity.HasOne(d => d.campaign).WithMany(p => p.VaccinConfirmations)
                .HasForeignKey(d => d.campaign_id)
                .HasConstraintName("VaccinConfirmations_ibfk_3");

            entity.HasOne(d => d.parent).WithMany(p => p.VaccinConfirmations)
                .HasForeignKey(d => d.parent_id)
                .HasConstraintName("VaccinConfirmations_ibfk_1");

            entity.HasOne(d => d.student).WithMany(p => p.VaccinConfirmations)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("VaccinConfirmations_ibfk_2");
        });

        modelBuilder.Entity<VaccinDocument>(entity =>
        {
            entity.HasKey(e => e.vaccin_document_id).HasName("PRIMARY");

            entity.HasIndex(e => e.campaign_id, "campaign_id");

            entity.HasIndex(e => e.nurse_id, "nurse_id");

            entity.HasIndex(e => e.student_id, "student_id");

            entity.HasIndex(e => e.vaccin_confirm_id, "vaccin_confirm_id");

            entity.Property(e => e.adverse_symptoms).HasColumnType("text");
            entity.Property(e => e.injection_time).HasColumnType("datetime");
            entity.Property(e => e.post_condition_vaccin).HasColumnType("text");
            entity.Property(e => e.pre_condition_vaccin).HasColumnType("text");
            entity.Property(e => e.status).HasColumnType("bit(1)");

            entity.HasOne(d => d.campaign).WithMany(p => p.VaccinDocuments)
                .HasForeignKey(d => d.campaign_id)
                .HasConstraintName("VaccinDocuments_ibfk_2");

            entity.HasOne(d => d.nurse).WithMany(p => p.VaccinDocuments)
                .HasForeignKey(d => d.nurse_id)
                .HasConstraintName("VaccinDocuments_ibfk_4");

            entity.HasOne(d => d.student).WithMany(p => p.VaccinDocuments)
                .HasForeignKey(d => d.student_id)
                .HasConstraintName("VaccinDocuments_ibfk_1");

            entity.HasOne(d => d.vaccin_confirm).WithMany(p => p.VaccinDocuments)
                .HasForeignKey(d => d.vaccin_confirm_id)
                .HasConstraintName("VaccinDocuments_ibfk_3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
