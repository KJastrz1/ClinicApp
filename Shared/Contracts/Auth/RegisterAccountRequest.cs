namespace Shared.Contracts.Auth;

public sealed record RegisterAccountRequest(
    string Email,
    string Password
);
