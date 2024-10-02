using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;

internal sealed class RemovePermissionsFromRoleCommandHandler : ICommandHandler<RemovePermissionsFromRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionReadRepository _permissionReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePermissionsFromRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionReadRepository permissionReadRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionReadRepository = permissionReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RemovePermissionsFromRoleCommand request, CancellationToken cancellationToken)
    {
        RoleId roleId = RoleId.Create(request.Id).Value;
       
        Role? role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
        if (role == null)
        {
            return Result.Failure<Guid>(RoleErrors.NotFound(roleId));
        }
      
        foreach (int permissionId in request.PermissionsIds)
        {
            Permission? permission = await _permissionReadRepository.GetByIdAsync(permissionId, cancellationToken);
            if (permission == null)
            {
                return Result.Failure<Guid>(PermissionErrors.NotFound(permissionId));
            }

            role.RemovePermission(permission); 
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}
