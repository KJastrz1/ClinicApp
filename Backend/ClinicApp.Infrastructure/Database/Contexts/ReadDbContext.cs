using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Infrastructure.Database.Configurations.Read;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class ReadDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;

    public ReadDbContext(DbContextOptions<ReadDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IWriteEntityConfiguration<>)
            )
        );
    }

    public IQueryable<Account> AccountsQueryable => Accounts.AsNoTracking();
    public IQueryable<Patient> PatientsQueryable => Patients.AsNoTracking();

    public override int SaveChanges()
    {
        throw new InvalidOperationException("ReadDbContext does not support SaveChanges.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("ReadDbContext does not support SaveChangesAsync.");
    }
}
