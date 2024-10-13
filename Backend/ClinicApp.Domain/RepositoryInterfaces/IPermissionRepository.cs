using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;

namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken);
}
