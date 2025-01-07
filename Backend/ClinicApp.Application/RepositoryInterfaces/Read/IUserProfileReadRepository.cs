using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using Shared.Contracts.UserProfile.Responses;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IUserProfileReadRepository
{
    Task<UserProfileResponse?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
}
