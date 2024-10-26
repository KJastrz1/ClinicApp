using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Role.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using Shared.Contracts;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class PermissionReadRepository : IPermissionReadRepository
{
    private readonly ReadDbContext _context;

    public PermissionReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<PermissionResponse?> GetByIdAsync(int permissionId, CancellationToken cancellationToken)
    {
        PermissionReadModel? permissionReadModel = await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);

        return permissionReadModel?.ToResponse();
    }

    public async Task<List<PermissionResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<PermissionReadModel> permissions = await _context.Permissions
            .ToListAsync(cancellationToken);

        return permissions.Select(p => p.ToResponse()).ToList();
    }

    public async Task<List<PermissionResponse>> GetByFilterAsync(PermissionFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<PermissionReadModel> query = _context.Permissions;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<PermissionResponse> permissions = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => p.ToResponse())
            .ToListAsync(cancellationToken);

        return permissions;
    }
}
