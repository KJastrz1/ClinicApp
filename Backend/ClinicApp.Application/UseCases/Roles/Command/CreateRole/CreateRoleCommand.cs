using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Roles.Command.CreateRole;

public sealed record CreateRoleCommand(
    string Name,
    List<int> PermissionsIds
) : ICommand<Guid>;
