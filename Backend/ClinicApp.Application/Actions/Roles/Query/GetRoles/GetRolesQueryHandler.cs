using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Shared;
using Shared.Contracts;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetRoles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, PagedResult<RoleResponse>>
{
    private readonly IRoleReadDapperRepository _roleReadRepository;

    public GetRolesQueryHandler(IRoleReadDapperRepository roleReadRepository)
    {
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<PagedResult<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        PagedResult<RoleResponse> result = await _roleReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return Result.Success(result);
    }
}
