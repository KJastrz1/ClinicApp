using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class PermissionReadRepository : IPermissionReadRepository
{
    private readonly ReadDbContext _context;

    public PermissionReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(int permissionId, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .ToListAsync(cancellationToken);
    }
}
