namespace Shared.Contracts.Auth.Requests;

public record RegisterRequest(
    string Email,
    string Password
);
