using Frontend.Api;
using Frontend.Api.Interfaces;
using Frontend.Auth;
using Frontend.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClientInterceptor>();

builder.Services.AddHttpClient("BackendAPI", client =>
{
    string? baseAddress = builder.Configuration["ApiBaseUrl"];
    if (string.IsNullOrEmpty(baseAddress))
    {
        throw new InvalidOperationException("ApiBaseUrl is not set in appsettings.json");
    }

    client.BaseAddress = new Uri(baseAddress);

}).AddHttpMessageHandler<HttpClientInterceptor>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendAPI"));

builder.Services.AddScoped<IAuthApi, AuthApi>();

builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    provider => provider.GetRequiredService<CustomAuthenticationStateProvider>()
);

builder.Services.AddAuthorizationCore();


WebAssemblyHost host = builder.Build();

CustomAuthenticationStateProvider authProvider = host.Services.GetRequiredService<CustomAuthenticationStateProvider>();
await authProvider.InitializeAsync();

await host.RunAsync();

