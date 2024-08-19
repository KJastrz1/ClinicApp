using ClinicApp.Domain.Models.Patients;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PatientEntity> Patients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
