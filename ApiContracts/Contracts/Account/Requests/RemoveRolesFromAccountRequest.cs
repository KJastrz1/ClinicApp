namespace Shared.Contracts.Account.Requests;

public sealed record RemoveRolesFromAccountRequest(
    List<Guid> RolesIds
);
