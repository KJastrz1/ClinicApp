using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Accounts.Command.RegisterAccount;

public sealed record RegisterAccountCommand(
    string Email,
    string Password)
    : ICommand<Guid>;
