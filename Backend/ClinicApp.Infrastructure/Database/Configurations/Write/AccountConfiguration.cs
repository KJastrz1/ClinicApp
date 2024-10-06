using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class AccountConfiguration : IWriteEntityConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TableNames.Accounts);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value).HasMaxLength(Email.MaxLength)
            .IsRequired();

        builder.Property(a => a.PasswordHash)
            .HasConversion(
                passwordHash => passwordHash.Value,
                value => PasswordHash.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.IsActivated)
            .IsRequired();

        builder.Property(a => a.CreatedOnUtc)
            .IsRequired();

        builder.Property(a => a.ModifiedOnUtc);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder
            .HasMany(a => a.Roles)
            .WithMany()
            .UsingEntity<AccountRole>();
    }
}
