// using ClinicApp.Domain.Models.Accounts;
// using ClinicApp.Domain.Models.Roles;
// using ClinicApp.Infrastructure.Database.Contexts;
// using ClinicApp.Infrastructure.Database.ReadModels.Auth;
// using Microsoft.EntityFrameworkCore;
//
// namespace ClinicApp.Infrastructure.Authentication;
//
// public class PermissionService : IPermissionService
// {
//     private readonly ReadDbContext _context;
//
//     public PermissionService(ReadDbContext context)
//     {
//         _context = context;
//     }
//
//     public async Task<HashSet<string>> GetPermissionsAsync(Guid accountId)
//     {
//         UserReadModel? account = await _context.Accounts
//             .Include(x => x.Roles)
//             .ThenInclude(x => x.Permissions)
//             .FirstOrDefaultAsync(x => x.Id == accountId);
//
//
//         if (account == null)
//         {
//             return new HashSet<string>();
//         }
//
//         return account.Roles
//             .SelectMany(role => role.Permissions)
//             .Select(permission => permission.Name)
//             .ToHashSet();
//     }
// }
