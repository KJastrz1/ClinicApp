using ClinicApp.Domain.Enums;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public class RoleSeeder : IDataSeeder
{
    private readonly RoleManager<Role> _roleManager;

    public RoleSeeder(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        foreach (UserRole roleName in Enum.GetValues<UserRole>())
        {
            if (!await _roleManager.RoleExistsAsync(roleName.ToString()))
            {
                await _roleManager.CreateAsync(new Role { Name = roleName.ToString() });
            }
        }
    }
}
