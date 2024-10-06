using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class RolePermissionConfiguration
    : IWriteEntityConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.RoleId)
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value).Value);


        builder.Property(p => p.PermissionId)
            .HasConversion(
                id => id.Value,
                value => PermissionId.Create(value).Value);

        builder.ToTable(TableNames.RolePermissions);
    }
}
