// using ClinicApp.Infrastructure.Database.Configurations.Read;
// using ClinicApp.Infrastructure.Database.ReadModels;
// using Microsoft.EntityFrameworkCore;
//
// namespace ClinicApp.Infrastructure.Database.Contexts;
//
// public class ReadDbContext : DbContext
// {
//     public DbSet<UserReadModel> Users { get; set; }
//     public DbSet<PatientReadModel> Patients { get; set; }
//
//     public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
//     {
//     }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(
//             AssemblyReference.Assembly,
//             t => t.GetInterfaces().Any(i =>
//                 i.IsGenericType &&
//                 i.GetGenericTypeDefinition() == typeof(IReadEntityConfiguration<>)));
//     }
// }
