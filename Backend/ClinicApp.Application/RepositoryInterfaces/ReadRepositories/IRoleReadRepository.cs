using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.ReadRepositories;

public interface IRoleReadRepository
{
    Task<RoleResponse?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken);

    Task<RoleResponse?> GetByNameAsync(RoleName roleName, CancellationToken cancellationToken);

    Task<PagedItems<RoleResponse>> GetByFilterAsync(RoleFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}
