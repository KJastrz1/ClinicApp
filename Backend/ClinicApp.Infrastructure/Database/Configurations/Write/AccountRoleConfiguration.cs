using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        builder.HasKey(x => new { x.AccountId, x.RoleId });
        
        builder.ToTable(TableNames.AccountRoles);
    }
}
