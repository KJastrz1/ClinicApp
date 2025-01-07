using System.Security.Claims;
using Frontend.Api.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Contracts.Auth.Requests;
using Shared.Contracts.Auth.Responses;

namespace Frontend.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
{
    private ClaimsIdentity? _cachedIdentity;
    private readonly IAuthApi _authApi;

    public ClaimsIdentity? Identity => _cachedIdentity;
    public bool IsCheckingAuth { get; private set; } = true;

    public CustomAuthenticationStateProvider(IAuthApi authApi)
    {
        _authApi = authApi;
        _ = InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        try
        {
            IsCheckingAuth = true;
            UserResponse? user = await _authApi.GetCurrentUserAsync();
            _cachedIdentity = user != null ? CreateIdentity(user) : null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch
        {
            _cachedIdentity = null;
        }
        finally
        {
            IsCheckingAuth = false;
        }
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedIdentity != null)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_cachedIdentity)));
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    }

    public async Task<bool> Login(LoginRequest loginRequest, bool keepLoggedIn)
    {
        UserResponse? user = await _authApi.LoginAsync(loginRequest, keepLoggedIn);
        if (user != null)
        {
            _cachedIdentity = CreateIdentity(user);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }

        return false;
    }

    public async Task Logout()
    {
        await _authApi.LogoutAsync();
        _cachedIdentity = null;
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    private ClaimsIdentity CreateIdentity(UserResponse user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("IsEmailConfirmed", user.IsEmailConfirmed.ToString())
        };

        claims.AddRange(user.Role.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims, "IdentityCookie");
    }
}
