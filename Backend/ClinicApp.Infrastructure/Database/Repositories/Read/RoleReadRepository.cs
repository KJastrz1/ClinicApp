using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class RoleReadRepository : IRoleReadRepository
{
    private readonly ReadDbContext _context;

    public RoleReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(RoleName roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name.Equals(roleName), cancellationToken);
    }
}
