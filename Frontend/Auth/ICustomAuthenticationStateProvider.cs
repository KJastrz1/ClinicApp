using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Contracts.Auth.Requests;

namespace Frontend.Auth;

public interface ICustomAuthenticationStateProvider
{
    ClaimsIdentity? Identity { get; }
    Task<bool> Login(LoginRequest loginRequest, bool keepLoggedIn);
    Task Logout();
    Task<AuthenticationState> GetAuthenticationStateAsync();
}
