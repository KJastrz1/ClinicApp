using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetRoleById;

internal sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IRoleReadDapperRepository _roleReadRepository;

    public GetRoleByIdQueryHandler(IRoleReadDapperRepository roleReadRepository)
    {
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        RoleResponse? role = await _roleReadRepository.GetByIdAsync(RoleId.Create(request.Id).Value, cancellationToken);

        if (role is null)
        {
            return Result.Failure<RoleResponse>(RoleErrors.NotFound(request.Id));
        }

        return role;
    }
}