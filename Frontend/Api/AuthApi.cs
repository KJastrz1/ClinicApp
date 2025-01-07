using System.Net.Http.Json;
using Frontend.Api.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Contracts.Auth.Requests;
using Shared.Contracts.Auth.Responses;

namespace Frontend.Api;

public class AuthApi : IAuthApi
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager NavigationManager;

    public AuthApi(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        NavigationManager = navigationManager;
    }

    public async Task<UserResponse?> GetCurrentUserAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("users/me");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        return null;
    }

    public async Task<UserResponse?> LoginAsync(LoginRequest loginRequest, bool keepLoggedIn)
    {
        string queryParams = $"useCookies=true&useSessionCookies={!keepLoggedIn}";
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"login?{queryParams}", loginRequest);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        return null;
    }

    public async Task LogoutAsync()
    {
        await _httpClient.PostAsync("users/logout", null);
        NavigationManager.NavigateTo("/");
    }
    
    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("register", request);
        return response.IsSuccessStatusCode;
    }
}
