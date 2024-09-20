// using ClinicApp.Infrastructure.Database.Constants;
// using ClinicApp.Infrastructure.Database.ReadModels;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace ClinicApp.Infrastructure.Database.Configurations.Read;
//
// public class UserBaseReadConfiguration : IReadEntityConfiguration<UserReadModel>
// {
//     public void Configure(EntityTypeBuilder<UserReadModel> builder)
//     {
//         builder.ToTable(TableNames.Users);
//
//         builder.HasKey(u => u.Id);
//
//         builder.Property(u => u.Id)
//             .HasColumnName("Id")
//             .IsRequired();
//
//         builder.Property(u => u.Email)
//             .HasColumnName("Email")
//             .IsRequired();
//
//         builder.Property(u => u.FirstName)
//             .HasColumnName("FirstName")
//             .IsRequired();
//
//         builder.Property(u => u.LastName)
//             .HasColumnName("LastName")
//             .IsRequired();
//
//         builder.Property(u => u.IsActivated)
//             .HasColumnName("IsActivated")
//             .IsRequired();
//
//         builder.Property(u => u.CreatedOnUtc)
//             .HasColumnName("CreatedOnUtc")
//             .IsRequired();
//
//         builder.Property(u => u.ModifiedOnUtc)
//             .HasColumnName("ModifiedOnUtc")
//             .IsRequired(false);
//
//         builder.Ignore("UserType");
//     }
// }
