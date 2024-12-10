using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly WriteDbContext _writeContext;

    public UserProfileRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<bool> ExistsAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await _writeContext.UserProfiles
            .AnyAsync(up => up.Id == userId, cancellationToken);
    }
}
