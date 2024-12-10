using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ClinicApp.Application.Abstractions.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public string Id => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string UserName => User?.FindFirst(ClaimTypes.Name)?.Value;

    public string Email => User?.FindFirst(ClaimTypes.Email)?.Value;

    public string PhoneNumber => User?.FindFirst(ClaimTypes.MobilePhone)?.Value;
}
