using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Roles.Command.RemovePermissionsFromRole;

internal sealed class RemovePermissionsFromRoleCommandHandler : ICommandHandler<RemovePermissionsFromRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePermissionsFromRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RemovePermissionsFromRoleCommand request,
        CancellationToken cancellationToken)
    {
        RoleId roleId = RoleId.Create(request.Id).Value;

        Role? role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
        if (role == null)
        {
            return Result.Failure<Guid>(RoleErrors.NotFound(roleId));
        }


        foreach (int permissionId in request.PermissionsIds)
        {
            Permission? permission =
                await _permissionRepository.GetByIdAsync(PermissionId.Create(permissionId).Value, cancellationToken);
            if (permission == null)
            {
                return Result.Failure<Guid>(PermissionErrors.NotFound(PermissionId.Create(permissionId).Value));
            }

            role.RemovePermission(permission);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}
