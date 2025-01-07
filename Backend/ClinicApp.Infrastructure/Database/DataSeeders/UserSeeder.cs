using Microsoft.AspNetCore.Identity;
using ClinicApp.Domain.Enums;
using ClinicApp.Infrastructure.Authentication.IdentityCore;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public class UserSeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserSeeder(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        foreach (UserRoleEnum role in Enum.GetValues<UserRoleEnum>())
        {
            await EnsureRoleExistsAsync(role.ToString());
        }


        for (int i = 1; i <= 100; i++)
        {
            string email = $"doctor{i}@email.com";
            await EnsureUserExistsAsync(email, "Haslo123!", UserRoleEnum.Doctor);
        }


        for (int i = 1; i <= 100; i++)
        {
            string email = $"patient{i}@email.com";
            await EnsureUserExistsAsync(email, "Haslo123!", UserRoleEnum.Patient);
        }


        for (int i = 1; i <= 5; i++)
        {
            string email = $"admin{i}@email.com";
            await EnsureUserExistsAsync(email, "Haslo123!", UserRoleEnum.Admin);
        }
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var role = new Role { Name = roleName };
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task EnsureUserExistsAsync(string email, string password, UserRoleEnum role)
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new User()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role.ToString());
            }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to create user '{email}'. Errors: {string.Join(", ", result.Errors)}");
            }
        }
        else if (!await _userManager.IsInRoleAsync(user, role.ToString()))
        {
            await _userManager.AddToRoleAsync(user, role.ToString());
        }
    }
}
