using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Configurations.Read;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class ReadDbContext : DbContext
{
    private DbSet<Account> AccountsDbSet { get; set; }
    private DbSet<Patient> PatientsDbSet { get; set; } 
    private DbSet<Clinic> ClinicsDbSet { get; set; } 
    private DbSet<Doctor> DoctorsDbSet { get; set; } 
    private DbSet<Role> RolesDbSet { get; set; } 
    private DbSet<Permission> PermissionsDbSet { get; set; }
    

    public IQueryable<Account> Accounts => AccountsDbSet.AsNoTracking();
    public IQueryable<Patient> Patients => PatientsDbSet.AsNoTracking();
    public IQueryable<Clinic> Clinics => ClinicsDbSet.AsNoTracking();
    public IQueryable<Doctor> Doctors => DoctorsDbSet.AsNoTracking();
    public IQueryable<Role> Roles => RolesDbSet.AsNoTracking();
    public IQueryable<Permission> Permissions => PermissionsDbSet.AsNoTracking();

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

    public override int SaveChanges()
    {
        throw new InvalidOperationException("ReadDbContext does not support SaveChanges.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("ReadDbContext does not support SaveChangesAsync.");
    }
}
