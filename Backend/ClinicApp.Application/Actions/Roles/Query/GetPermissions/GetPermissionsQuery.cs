using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetPermissions;

public sealed record GetPermissionsQuery : IQuery<List<PermissionResponse>>
{
    public PermissionFilter Filter { get; init; } = new PermissionFilter();
}
