using ClinicApp.Domain.Models.Roles.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.ReadRepositories.Dapper;

public interface IRoleReadDapperRepository
{
    Task<RoleResponse?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken);

    Task<PagedResult<RoleResponse>> GetByFilterAsync(
        RoleFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
