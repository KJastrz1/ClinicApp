using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Roles.Command.RemovePermissionsFromRole;

public sealed record RemovePermissionsFromRoleCommand(
    Guid Id,
    List<int> PermissionsIds
) : ICommand<Guid>;