using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Accounts.Command.AddRolesToAccount;

public sealed record AddRolesToAccountCommand(
    Guid AccountId,
    List<Guid> RolesIds)
    : ICommand<Guid>;
