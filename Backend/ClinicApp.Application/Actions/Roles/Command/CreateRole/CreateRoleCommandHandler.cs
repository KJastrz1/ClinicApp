using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Permissions;
using ClinicApp.Domain.Models.Permissions.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Roles.Command.CreateRole;

internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork,
        IRoleReadRepository roleReadRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<RoleName> roleNameResult = RoleName.Create(request.Name);
        if (roleNameResult.IsFailure)
        {
            return Result.Failure<Guid>(roleNameResult.Error);
        }


        Role? existingRole = await _roleReadRepository.GetByNameAsync(roleNameResult.Value, cancellationToken);
        if (existingRole is not null)
        {
            return Result.Failure<Guid>(RoleErrors.NameAlreadyExists);
        }

        List<Permission> permissions = new();
        foreach (int permissionIdValue in request.PermissionsIds)
        {
            PermissionId permissionId = PermissionId.Create(permissionIdValue).Value;
            Permission? permission = await _permissionRepository.GetByIdAsync(permissionId, cancellationToken);
            if (permission == null)
            {
                return Result.Failure<Guid>(PermissionErrors.NotFound(permissionId));
            }

            permissions.Add(permission);
        }

        var role = Role.Create(RoleId.New(), roleNameResult.Value, permissions);

        _roleRepository.Add(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}
