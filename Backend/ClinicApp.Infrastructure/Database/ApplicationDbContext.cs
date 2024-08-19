using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.UserBase;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PatientBase> Patients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
