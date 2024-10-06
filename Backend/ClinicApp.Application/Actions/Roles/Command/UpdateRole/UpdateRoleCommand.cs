using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Roles.Command.CreateRole;

public sealed record CreateRoleCommand(
    string Name,
    List<int> PermissionsIds
) : ICommand<Guid>;
