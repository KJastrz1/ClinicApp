using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Database.ReadModels;
using Shared.Contracts.UserProfile.Responses;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class UserProfileReadRepository : IUserProfileReadRepository
{
    private readonly ReadDbContext _context;

    public UserProfileReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileResponse?> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        UserProfileReadModel? userProfileReadModel = await _context.UserProfilesSet
            .FirstOrDefaultAsync(u => u.Id.Equals(userId.Value), cancellationToken);

        return userProfileReadModel?.MapToResponse();
    }
}
