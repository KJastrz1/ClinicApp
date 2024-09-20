using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.ToTable(TableNames.RolePermissions);
        
        builder.HasData(
            Create(Role.Admin, PermissionEnum.AddRole),
            Create(Role.Admin, PermissionEnum.RemoveRole),
            Create(Role.Admin, PermissionEnum.ReadPatient),
            Create(Role.Admin, PermissionEnum.CreatePatient),
            Create(Role.Admin, PermissionEnum.UpdatePatient),
            Create(Role.Admin, PermissionEnum.DeletePatient),
            Create(Role.Admin, PermissionEnum.ReadDoctor),
            Create(Role.Admin, PermissionEnum.CreateDoctor),
            Create(Role.Admin, PermissionEnum.UpdateDoctor),
            Create(Role.Admin, PermissionEnum.DeleteDoctor));
    }

    private static RolePermission Create(
        Role role, PermissionEnum permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
