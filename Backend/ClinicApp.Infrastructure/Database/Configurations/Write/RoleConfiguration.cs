using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        // builder.HasMany(x => x.Accounts)
        //     .WithMany(x=>x.Roles)
        //     .UsingEntity<AccountRole>();

        builder.HasData(Role.GetValues());
    }
}
