using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public class RoleSeeder : IEFDataSeeder
{
    public void Seed(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedPermissions(modelBuilder);
        SeedRolePermissions(modelBuilder);
    }

    private void SeedRoles(ModelBuilder modelBuilder)
    {
        Role[] roles = BasicRoles.All
            .Select(r => Role.Create(RoleId.Create(r.Id).Value, RoleName.Create(r.Name).Value))
            .ToArray();

        modelBuilder.Entity<Role>().HasData(roles);
    }

    private void SeedPermissions(ModelBuilder modelBuilder)
    {
        IEnumerable<Permission> permissions = Enum
            .GetValues<PermissionEnum>()
            .Select(p => new Permission
            {
                Id = PermissionId.Create((int)p).Value,
                Name = p.ToString()
            });

        modelBuilder.Entity<Permission>().HasData(permissions);
    }

    private void SeedRolePermissions(ModelBuilder modelBuilder)
    {
        var rolePermissions = new List<RolePermission>();

        foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)).Cast<PermissionEnum>())
        {
            rolePermissions.Add(new RolePermission
            {
                RoleId = RoleId.Create(BasicRoles.SuperAdmin.Id).Value,
                PermissionId = PermissionId.Create((int)permission).Value
            });
        }

        IEnumerable<PermissionEnum> adminPermissions = Enum.GetValues(typeof(PermissionEnum))
            .Cast<PermissionEnum>()
            .Where(p => p != PermissionEnum.AssignRole &&
                        p != PermissionEnum.RevokeRole &&
                        p != PermissionEnum.CreateRole &&
                        p != PermissionEnum.DeleteRole &&
                        p != PermissionEnum.AddPermissionToRole &&
                        p != PermissionEnum.RemovePermissionFromRole);

        foreach (PermissionEnum permission in adminPermissions)
        {
            rolePermissions.Add(new RolePermission
            {
                RoleId = RoleId.Create(BasicRoles.Admin.Id).Value,
                PermissionId = PermissionId.Create((int)permission).Value
            });
        }

        PermissionEnum[] doctorPermissions = new[]
        {
            PermissionEnum.ReadPatient,
            PermissionEnum.CreatePatient,
            PermissionEnum.UpdatePatient,
            PermissionEnum.DeletePatient,
            PermissionEnum.ReadDoctor
        };

        foreach (PermissionEnum permission in doctorPermissions)
        {
            rolePermissions.Add(new RolePermission
            {
                RoleId = RoleId.Create(BasicRoles.Doctor.Id).Value,
                PermissionId = PermissionId.Create((int)permission).Value
            });
        }

        rolePermissions.Add(new RolePermission
        {
            RoleId = RoleId.Create(BasicRoles.Patient.Id).Value,
            PermissionId = PermissionId.Create((int)PermissionEnum.ReadDoctor).Value
        });

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }
}
