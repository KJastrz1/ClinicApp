using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Roles.Command.AddPermissionsToRole;

internal sealed class AddPermissionsToRoleCommandHandler : ICommandHandler<AddPermissionsToRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPermissionsToRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {
        RoleId roleId = RoleId.Create(request.Id).Value;

        Role? role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
        if (role == null)
        {
            return Result.Failure<Guid>(RoleErrors.NotFound(roleId));
        }

        foreach (int permissionId in request.PermissionsIds)
        {
            PermissionId permissionIdValue = PermissionId.Create(permissionId).Value;
            Permission? permission = await _permissionRepository.GetByIdAsync(permissionIdValue, cancellationToken);
            if (permission == null)
            {
                return Result.Failure<Guid>(PermissionErrors.NotFound(permissionIdValue));
            }

            role.AddPermission(permission);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}
