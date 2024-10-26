using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Roles.Query.GetRoles;

public sealed record GetRolesQuery : IQuery<PagedItems<RoleResponse>>
{
    public RoleFilter Filter { get; init; } = new RoleFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
