using ClinicApp.Infrastructure.Authentication.IdentityCore;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.App.Configuration;

public class AuthenticationAndAuthorizationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;
            })
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<WriteDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        // services.AddDataProtection();

        services.AddHttpContextAccessor();
    }
}
