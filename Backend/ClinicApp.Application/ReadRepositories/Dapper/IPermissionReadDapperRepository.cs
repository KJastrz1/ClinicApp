using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.ReadRepositories.Dapper;

public interface IPermissionReadDapperRepository
{
    Task<Permission?> GetByIdAsync(int Id, CancellationToken cancellationToken);

    Task<List<Permission>> GetByFilterAsync(
        PermissionFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
