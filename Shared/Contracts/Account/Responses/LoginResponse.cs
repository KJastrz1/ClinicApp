namespace Shared.Contracts.Account.Responses;

public record LoginResponse(
    string Token,
    AccountResponse Account
);
