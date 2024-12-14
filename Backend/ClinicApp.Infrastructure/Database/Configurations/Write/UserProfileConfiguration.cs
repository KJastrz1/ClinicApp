using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.UserProfiles;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class UserProfileConfiguration : IWriteEntityConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable(TableNames.UserProfiles);
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value);

        builder.Property(u => u.UserRole)
            .HasConversion(
                userType => userType.ToString(),
                value => Enum.Parse<UserRole>(value))
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => FirstName.Create(value).Value)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => LastName.Create(value).Value)
            .IsRequired();

        builder.Property(u => u.CreatedOnUtc)
            .IsRequired();

        builder.Property(u => u.ModifiedOnUtc);
    }
}
