using ClinicApp.Application.Abstractions.Messaging;

public sealed record RemovePermissionsFromRoleCommand(
    Guid Id,
    List<int> PermissionsIds
) : ICommand<Guid>;
