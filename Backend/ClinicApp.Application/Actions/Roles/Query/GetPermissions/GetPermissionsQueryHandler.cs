using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetPermissions;

internal sealed class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, List<PermissionResponse>>
{
    private readonly IPermissionReadDapperRepository _permissionReadRepository;

    public GetPermissionsQueryHandler(IPermissionReadDapperRepository permissionReadRepository)
    {
        _permissionReadRepository = permissionReadRepository;
    }

    public async Task<Result<List<PermissionResponse>>> Handle(GetPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        List<Permission> permissions = await _permissionReadRepository.GetByFilterAsync(
            request.Filter,
            pageNumber: 1,
            pageSize: int.MaxValue,
            cancellationToken);


        List<PermissionResponse> permissionResponses =
            permissions.ConvertAll(p => new PermissionResponse(p.Id, p.Name));

        return Result.Success(permissionResponses);
    }
}
