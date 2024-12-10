using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IRoleReadRepository
{
    Task<RoleResponse?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken);

    Task<RoleResponse?> GetByNameAsync(string roleName, CancellationToken cancellationToken);

    Task<PagedItems<RoleResponse>> GetByFilterAsync(RoleFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}
