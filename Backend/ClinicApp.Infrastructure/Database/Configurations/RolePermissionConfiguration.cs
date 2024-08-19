using ClinicApp.Domain.Models.User;
using ClinicApp.Domain.Models.UserBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = ClinicApp.Domain.Enums.Permission;

namespace ClinicApp.Infrastructure.Database.Configurations;

internal sealed class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Admin, Permission.CreatePatient),
            Create(Role.Admin, Permission.UpdatePatient));
    }

    private static RolePermission Create(
        Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
