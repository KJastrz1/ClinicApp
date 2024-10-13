using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;

namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(RoleId id, CancellationToken cancellationToken);
    void Add(Role role);
    void Update(Role role);
    void Remove(Role role);
}
