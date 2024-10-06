using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Accounts.Command.AddRolesToAccount;

public sealed record AddRolesToAccountCommand(
    Guid AccountId,
    List<Guid> RolesIds)
    : ICommand<Guid>;
