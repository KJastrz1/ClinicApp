using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Roles.Command.RemovePermissionsFromRole;

public sealed record RemovePermissionsFromRoleCommand(
    Guid Id,
    List<int> PermissionsIds
) : ICommand<Guid>;
