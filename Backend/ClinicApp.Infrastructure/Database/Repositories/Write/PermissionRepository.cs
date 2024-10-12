using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class PermissionRepository : IPermissionRepository
{
    private readonly WriteDbContext _writeContext;

    public PermissionRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }
  
    public async Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Permissions
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
