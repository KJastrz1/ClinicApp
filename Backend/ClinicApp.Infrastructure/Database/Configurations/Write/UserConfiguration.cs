using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value);

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
            .HasOne(up => up.Account)
            .WithOne(a => a.User)
            .HasForeignKey<User>(up => up.AccountId);

        builder
            .Property(up => up.AccountId)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value).Value);
    }
}
