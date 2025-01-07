using ClinicApp.Infrastructure.Database.Configurations.Read;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using ClinicApp.Infrastructure.Database.Constants;
using ClinicApp.Infrastructure.Database.ReadModels;
using Microsoft.EntityFrameworkCore;


namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class ReadDbContext : DbContext
{
    private DbSet<UserProfileReadModel> UserProfiles { get; set; }
    private DbSet<EmployeeReadModel> Employees { get; set; }
    private DbSet<PatientProfileReadModel> Patients { get; set; }
    private DbSet<DoctorReadModel> Doctors { get; set; }
    private DbSet<SpecialtyReadModel> Specialties { get; set; }
    private DbSet<ClinicReadModel> Clinics { get; set; }
    private DbSet<AppointmentReadModel> Appointments { get; set; }
    private DbSet<EmployeeLeaveReadModel> EmployeeLeaves { get; set; }


    internal IQueryable<UserProfileReadModel> UserProfilesSet => UserProfiles.AsNoTracking();
    internal IQueryable<EmployeeReadModel> EmployeesSet => Employees.AsNoTracking();
    internal IQueryable<PatientProfileReadModel> PatientsSet => Patients.AsNoTracking();
    internal IQueryable<DoctorReadModel> DoctorsSet => Doctors.AsNoTracking();
    internal IQueryable<SpecialtyReadModel> SpecialtiesSet => Specialties.AsNoTracking();
    internal IQueryable<ClinicReadModel> ClinicsSet => Clinics.AsNoTracking();

    internal IQueryable<AppointmentReadModel> AppointmentsSet => Appointments.AsNoTracking();
    internal IQueryable<EmployeeLeaveReadModel> EmployeeLeavesSet => EmployeeLeaves.AsNoTracking();

    public ReadDbContext(DbContextOptions<ReadDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IWriteEntityConfiguration<>)
            )
        );
      
        modelBuilder.Entity<AppointmentReadModel>().ToTable(TableNames.Appointments);
        modelBuilder.Entity<ClinicReadModel>().ToTable(TableNames.Clinics);
        modelBuilder.Entity<DoctorReadModel>().ToTable(TableNames.Doctors);
        modelBuilder.Entity<SpecialtyReadModel>().ToTable(TableNames.Specialties);
        modelBuilder.Entity<PatientProfileReadModel>().ToTable(TableNames.Patients);
        modelBuilder.Entity<UserProfileReadModel>().ToTable(TableNames.UserProfiles);
        modelBuilder.Entity<EmployeeReadModel>().ToTable(TableNames.Employees);
        modelBuilder.Entity<EmployeeLeaveReadModel>().ToTable(TableNames.EmployeeLeaves);
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
