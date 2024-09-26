namespace Shared.Contracts.Account.Requests;

public sealed record RegisterAccountRequest(
    string Email,
    string Password
);
