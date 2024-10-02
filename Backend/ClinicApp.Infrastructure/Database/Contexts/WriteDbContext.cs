using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using Microsoft.EntityFrameworkCore;
using ClinicApp.Infrastructure.Database.DataSeeders;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyConfigurations(modelBuilder);
        ApplySeedersFromAssembly(modelBuilder);
    }

    private void ApplySeedersFromAssembly(ModelBuilder modelBuilder)
    {
        Type seederType = typeof(IEFDataSeeder);
        IEnumerable<IEFDataSeeder> seeders = AssemblyReference.Assembly
            .GetTypes()
            .Where(t => seederType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEFDataSeeder>();

        foreach (IEFDataSeeder seeder in seeders)
        {
            seeder.Seed(modelBuilder);
        }
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IWriteEntityConfiguration<>)));
    }
}
