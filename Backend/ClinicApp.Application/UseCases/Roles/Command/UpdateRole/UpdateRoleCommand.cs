using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Roles.Command.UpdateRole;

public sealed record UpdateRoleCommand(
    Guid Id,
    string Name
) : ICommand<Guid>;
