using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Infrastructure.Database.Configurations.Read;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            t => t.GetInterfaces().Any(i => 
                i.IsGenericType && 
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                i.GetGenericTypeDefinition() != typeof(IReadEntityConfiguration<>)));
    }
}
