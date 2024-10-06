using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Roles;

namespace ClinicApp.Application.ReadRepositories;

public interface IPermissionReadRepository
{
    Task<Permission?> GetByIdAsync(int permissionId, CancellationToken cancellationToken);

    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken);
}
