using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class AccountRoleConfiguration : IWriteEntityConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        builder.HasKey(x => new { x.AccountId, x.RoleId });

        builder.ToTable(TableNames.AccountRoles);

        builder.Property(a => a.AccountId)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.RoleId)
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value).Value)
            .IsRequired();
    }
}
