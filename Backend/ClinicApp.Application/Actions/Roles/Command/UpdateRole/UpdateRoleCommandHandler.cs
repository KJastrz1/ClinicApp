using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Roles.Command.CreateRole;

internal sealed class UpdateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionReadRepository _permissionReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionReadRepository permissionReadRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionReadRepository = permissionReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<RoleName> roleNameResult = RoleName.Create(request.Name);
        if (roleNameResult.IsFailure)
        {
            return Result.Failure<Guid>(roleNameResult.Error);
        }


        List<Permission> permissions = new();
        foreach (int permissionId in request.PermissionsIds)
        {
            Permission? permission = await _permissionReadRepository.GetByIdAsync(permissionId, cancellationToken);
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
