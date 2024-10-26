using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.UseCases.Roles.Query.GetPermissions;

internal sealed class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, List<PermissionResponse>>
{
    private readonly IPermissionReadRepository _permissionReadRepository;

    public GetPermissionsQueryHandler(IPermissionReadRepository permissionReadRepository)
    {
        _permissionReadRepository = permissionReadRepository;
    }

    public async Task<Result<List<PermissionResponse>>> Handle(GetPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        List<PermissionResponse> permissions = await _permissionReadRepository.GetByFilterAsync(
            request.Filter,
            pageNumber: 1,
            pageSize: int.MaxValue,
            cancellationToken);
        
        return permissions;
    }
}
