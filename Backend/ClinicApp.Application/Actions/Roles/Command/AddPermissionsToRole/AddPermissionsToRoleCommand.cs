using ClinicApp.Application.Abstractions.Messaging;

public sealed record AddPermissionsToRoleCommand(
    Guid Id,
    List<int> PermissionsIds
) : ICommand<Guid>;
