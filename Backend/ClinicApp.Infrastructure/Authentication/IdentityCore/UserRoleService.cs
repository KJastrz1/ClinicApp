using ClinicApp.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Infrastructure.Authentication.IdentityCore;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<User> _userManager;

    public UserRoleService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> AssignRoleToUserAsync(Guid userId, string role, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false;
        }

        IdentityResult result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }
}
