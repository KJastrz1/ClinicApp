namespace Shared.Contracts.Account.Requests;

public sealed record AddRolesToAccountRequest(
    List<Guid> RolesIds
);
