using ClinicApp.Application.Abstractions.Messaging;

public sealed record CreateRoleCommand(
 ) : ICommand<Guid>;
