using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Accounts.Command.RemoveRolesFromAccount;

public sealed record RemoveRolesFromAccountCommand(
    Guid AccountId,
    List<Guid> RolesIds)
    : ICommand<Guid>;
