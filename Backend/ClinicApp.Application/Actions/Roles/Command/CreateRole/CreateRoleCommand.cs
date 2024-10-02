using ClinicApp.Application.Abstractions.Messaging;

public sealed record CreateRoleCommand(
    string Name,
    List<int> PermissionsIds
) : ICommand<Guid>;
