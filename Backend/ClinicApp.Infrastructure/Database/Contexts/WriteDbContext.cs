using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Configurations.Read;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IWriteEntityConfiguration<>)));
        
        ApplySeedersFromAssembly(modelBuilder);
    }

    private void ApplySeedersFromAssembly(ModelBuilder modelBuilder)
    {
        Type seederType = typeof(IDataSeeder);
        IEnumerable<IDataSeeder> seeders = AssemblyReference.Assembly
            .GetTypes()
            .Where(t => seederType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IDataSeeder>();

        foreach (IDataSeeder seeder in seeders)
        {
            seeder.Seed(modelBuilder);
        }
    }
}
