﻿using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.UserProfiles;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using ClinicApp.Infrastructure.Database.Configurations.Write;
using ClinicApp.Infrastructure.Database.DataSeeders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Contexts;

public sealed class WriteDbContext : IdentityDbContext<User, Role, Guid>
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
    }


    internal DbSet<User> Users { get; set; }
    internal DbSet<UserProfile> UserProfiles { get; set; }
    internal DbSet<Patient> Patients { get; set; }
    internal DbSet<Doctor> Doctors { get; set; }
    internal DbSet<Clinic> Clinics { get; set; }
    internal DbSet<Appointment> Appointments { get; set; }
    internal DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
    internal DbSet<Role> Roles { get; set; }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

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
