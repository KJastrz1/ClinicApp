using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.UseCases.Roles.Query.GetRoleById;

internal sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IRoleReadRepository _roleReadRepository;

    public GetRoleByIdQueryHandler(IRoleReadRepository roleReadRepository)
    {
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        RoleId roleId = RoleId.Create(request.Id).Value;

        RoleResponse? role = await _roleReadRepository.GetByIdAsync(roleId, cancellationToken);

        if (role is null)
        {
            return Result.Failure<RoleResponse>(RoleErrors.NotFound(roleId));
        }

        return role;
    }
}
