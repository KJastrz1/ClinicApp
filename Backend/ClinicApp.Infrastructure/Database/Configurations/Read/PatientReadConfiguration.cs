// using ClinicApp.Domain.Enums;
// using ClinicApp.Infrastructure.Database.ReadModels;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace ClinicApp.Infrastructure.Database.Configurations.Read;
//
// public class PatientReadConfiguration : IReadEntityConfiguration<PatientReadModel>
// {
//     public void Configure(EntityTypeBuilder<PatientReadModel> builder)
//     {
//         builder.Property(p => p.SocialSecurityNumber)
//             .HasColumnName("SocialSecurityNumber")
//             .IsRequired();
//
//         builder.Property(p => p.DateOfBirth)
//             .HasColumnName("DateOfBirth")
//             .IsRequired();
//     }
// }
