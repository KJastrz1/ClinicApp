using ClinicApp.Infrastructure.Database.Configurations.Write;
using ClinicApp.Infrastructure.Database.ReadModels;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using Microsoft.EntityFrameworkCore;
using UserReadModel = ClinicApp.Infrastructure.Database.ReadModels.Auth.UserReadModel;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class ReadDbContext : DbContext
{
    private DbSet<UserReadModel> UsersDbSet { get; set; }
    private DbSet<PatientReadModel> PatientsDbSet { get; set; } 
    private DbSet<ClinicReadModel> ClinicsDbSet { get; set; } 
    private DbSet<DoctorReadModel> DoctorsDbSet { get; set; } 
    private DbSet<AppointmentReadModel> AppointmentsDbSet { get; set; }
    private DbSet<EmployeeLeaveReadModel> EmployeeLeavesDbSet { get; set; }
    
    private DbSet<RoleReadModel> RolesDbSet { get; set; } 
    private DbSet<PermissionReadModel> PermissionsDbSet { get; set; }
    
    
    internal IQueryable<UserReadModel> Users => UsersDbSet.AsNoTracking();
    internal IQueryable<PatientReadModel> Patients => PatientsDbSet.AsNoTracking();
    internal IQueryable<ClinicReadModel> Clinics => ClinicsDbSet.AsNoTracking();
    internal IQueryable<DoctorReadModel> Doctors => DoctorsDbSet.AsNoTracking();
    internal IQueryable<AppointmentReadModel> Appointments => AppointmentsDbSet.AsNoTracking();
    internal IQueryable<EmployeeLeaveReadModel> EmployeeLeaves => EmployeeLeavesDbSet.AsNoTracking();
    
    internal IQueryable<RoleReadModel> Roles => RolesDbSet.AsNoTracking();
    internal IQueryable<PermissionReadModel> Permissions => PermissionsDbSet.AsNoTracking();


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
