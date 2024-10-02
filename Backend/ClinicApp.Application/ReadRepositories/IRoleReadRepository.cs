using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;

namespace ClinicApp.Application.ReadRepositories;

public interface IRoleReadRepository
{
    Task<Role?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken);
}
