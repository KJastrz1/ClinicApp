using ClinicApp.Domain.Models.UserProfiles;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IUserProfileRepository
{
    Task<bool> ExistsAsync(UserId userId, CancellationToken cancellationToken);
}
