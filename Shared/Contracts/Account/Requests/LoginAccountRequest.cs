namespace Shared.Contracts.Account.Requests;

public sealed record LoginAccountRequest(
    string Email,
    string Password
);
