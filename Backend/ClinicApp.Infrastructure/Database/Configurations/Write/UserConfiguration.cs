using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class UserConfiguration : IWriteEntityConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value);

        builder.Property(u => u.UserType)
            .HasConversion(
                userType => userType.ToString(),
                value => Enum.Parse<UserType>(value))
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

        builder
            .HasOne(u => u.Account)
            .WithOne(a => a.User)
            .HasForeignKey<User>(u => u.AccountId)
            .IsRequired(false);

        builder
            .Property(u => u.AccountId)
            .HasConversion(
                id => id == null ? Guid.Empty : id.Value,
                value => value == Guid.Empty ? null : AccountId.Create(value).Value)
            .IsRequired(false);
    }
}
