using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Roles;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.ReadRepositories;

public interface IPermissionReadRepository
{
    Task<PermissionResponse?> GetByIdAsync(int permissionId, CancellationToken cancellationToken);

    Task<List<PermissionResponse>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<List<PermissionResponse>> GetByFilterAsync(PermissionFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
