using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Role.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using Shared.Contracts;
using Shared.Contracts.Shared;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class RoleReadRepository : IRoleReadRepository
{
    private readonly ReadDbContext _context;

    public RoleReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<RoleResponse?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken)
    {
        RoleReadModel? roleReadModel = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

        return roleReadModel?.ToResponse();
    }

    public async Task<RoleResponse?> GetByNameAsync(RoleName roleName, CancellationToken cancellationToken)
    {
        RoleReadModel? roleReadModel = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Name.Equals(roleName), cancellationToken);

        return roleReadModel?.ToResponse();
    }

    public async Task<PagedItems<RoleResponse>> GetByFilterAsync(RoleFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        IQueryable<RoleReadModel> query = _context.Roles
            .Include(r => r.Permissions);

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(r => r.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrWhiteSpace(filter.PermissionName))
        {
            query = query.Where(r => r.Permissions.Any(p => p.Name.Contains(filter.PermissionName)));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<RoleResponse> roles = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => r.ToResponse())
            .ToListAsync(cancellationToken);

        return new PagedItems<RoleResponse>
        {
            Items = roles,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
