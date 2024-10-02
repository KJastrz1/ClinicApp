using System.Security.Claims;
using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace ClinicApp.Infrastructure.Authentication;

public class UserContext : IUserContext
{
    private readonly IPermissionService _permissionService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IPermissionService permissionService, IHttpContextAccessor httpContextAccessor)
    {
        _permissionService = permissionService;
        _httpContextAccessor = httpContextAccessor;
    }

    public AccountId AccountId
    {
        get
        {
            string? userIdString = _httpContextAccessor.HttpContext?.User.FindFirst("AccountId")?.Value;
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return AccountId.Create(userId).Value;
            }

            throw new Exception("AccountId not found in context");
        }
    }

    public Email Email
    {
        get
        {
            string? email = _httpContextAccessor.HttpContext?.User.FindFirst("Email")?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                return Email.Create(email).Value;
            }

            throw new Exception("Email not found in context");
        }
    }

    public IEnumerable<RoleName> RoleNames
    {
        get
        {
            IEnumerable<Claim>? rolesClaim = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role);
            return rolesClaim.Select(c => RoleName.Create(c.Value).Value).ToList();
        }
    }

    public HashSet<string> Permissions
    {
        get
        {
            IEnumerable<Claim>? permissionsClaim = _httpContextAccessor.HttpContext?.User.FindAll("permission");
            return permissionsClaim.Select(c => c.Value).ToHashSet();
        }
    }
}