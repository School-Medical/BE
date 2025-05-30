using Microsoft.EntityFrameworkCore;

namespace SchoolMedicalSystem.Infrastructure.Data;

public partial class SchoolMedicalDbContext : DbContext
{
    public SchoolMedicalDbContext(DbContextOptions<SchoolMedicalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
