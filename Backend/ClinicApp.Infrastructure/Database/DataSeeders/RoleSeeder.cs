// using ClinicApp.Domain.Enums;
// using ClinicApp.Domain.Models.Roles;
// using ClinicApp.Domain.Models.Roles.ValueObjects;
// using Microsoft.EntityFrameworkCore;
//
// namespace ClinicApp.Infrastructure.Database.DataSeeders;
//
// public class RoleSeeder : IDataSeeder
// {
//     public void Seed(ModelBuilder modelBuilder)
//     {
//         SeedRoles(modelBuilder);
//         SeedPermissions(modelBuilder);
//         // SeedRolePermissions(modelBuilder);
//     }
//
//     private void SeedRoles(ModelBuilder modelBuilder)
//     {
//         Role[] roles = BasicRoles.All
//             .Select(r => Role.Create(RoleId.Create(r.Id).Value, RoleName.Create(r.Name).Value))
//             .ToArray();
//
//         modelBuilder.Entity<Role>().HasData(roles);
//     }
//
//     private void SeedPermissions(ModelBuilder modelBuilder)
//     {
//         IEnumerable<Permission> permissions = Enum
//             .GetValues<PermissionEnum>()
//             .Select(p => new Permission
//             {
//                 Id = (int)p,
//                 Name = p.ToString()
//             });
//
//         modelBuilder.Entity<Permission>().HasData(permissions);
//     }
//
//     private void SeedRolePermissions(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<RolePermission>().HasData(
//             Enum.GetValues(typeof(PermissionEnum))
//                 .Cast<PermissionEnum>()
//                 .Select(p => Create(BasicRoles.SuperAdmin.Id, p))
//                 .ToArray(),
//             Enum.GetValues(typeof(PermissionEnum))
//                 .Cast<PermissionEnum>()
//                 .Where(p => p != PermissionEnum.AssignAdmin && p != PermissionEnum.RemoveAdmin)
//                 .Select(p => Create(BasicRoles.Admin.Id, p))
//                 .ToArray(),
//             Create(BasicRoles.Doctor.Id, PermissionEnum.ReadPatient),
//             Create(BasicRoles.Doctor.Id, PermissionEnum.CreatePatient),
//             Create(BasicRoles.Doctor.Id, PermissionEnum.UpdatePatient),
//             Create(BasicRoles.Doctor.Id, PermissionEnum.DeletePatient),
//             Create(BasicRoles.Doctor.Id, PermissionEnum.ReadDoctor),
//             Create(BasicRoles.Patient.Id, PermissionEnum.ReadDoctor)
//         );
//     }
//
//     private static RolePermission Create(Guid roleId, PermissionEnum permission)
//     {
//         return new RolePermission
//         {
//             RoleId = RoleId.Create(roleId).Value,
//             PermissionId = (int)permission
//         };
//     }
// }
