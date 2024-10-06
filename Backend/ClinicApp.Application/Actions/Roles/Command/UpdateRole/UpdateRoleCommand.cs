using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Roles.Command.UpdateRole;

public sealed record UpdateRoleCommand(
    Guid Id,
    string Name
) : ICommand<Guid>;
