using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Roles.Query.GetRoles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, PagedItems<RoleResponse>>
{
    private readonly IRoleReadRepository _roleReadRepository;

    public GetRolesQueryHandler(IRoleReadRepository roleReadRepository)
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
