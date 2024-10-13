using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Shared;
using Shared.Contracts;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetRoles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, PagedItems<RoleResponse>>
{
    private readonly IRoleReadDapperRepository _roleReadRepository;

    public GetRolesQueryHandler(IRoleReadDapperRepository roleReadRepository)
    {
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<PagedItems<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        PagedItems<RoleResponse> items = await _roleReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return items;
    }
}
