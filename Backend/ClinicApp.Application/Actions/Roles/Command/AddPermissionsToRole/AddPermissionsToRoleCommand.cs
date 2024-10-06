using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Roles.Command.AddPermissionsToRole;

public sealed record AddPermissionsToRoleCommand(
    Guid Id,
    List<int> PermissionsIds
) : ICommand<Guid>;
