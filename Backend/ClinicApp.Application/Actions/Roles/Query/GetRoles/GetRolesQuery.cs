using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts;
using Shared.Contracts.Patient;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetRoles;

public sealed record GetRolesQuery : IQuery<PagedResult<RoleResponse>>
{
    public RoleFilter Filter { get; init; } = new RoleFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
