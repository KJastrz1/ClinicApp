using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Accounts.Command.RegisterAccount;

public sealed record RegisterAccountCommand(
    string Email,
    string Password)
    : ICommand<Guid>;
