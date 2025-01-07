using Shared.Contracts.Auth.Requests;
using Shared.Contracts.Auth.Responses;

namespace Frontend.Api.Interfaces;

public interface IAuthApi
{
    Task<UserResponse?> GetCurrentUserAsync();
    Task<UserResponse?> LoginAsync(LoginRequest loginRequest, bool keepLoggedIn);
    Task LogoutAsync();
    
    Task<bool> RegisterAsync(RegisterRequest request);
}
