using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.UseCases.Roles.Command.UpdateRole;

internal sealed class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork,
        IRoleReadRepository roleReadRepository)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        RoleId roleId = RoleId.Create(request.Id).Value;

        Role? role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
        if (role == null)
        {
            return Result.Failure<Guid>(RoleErrors.NotFound(roleId));
        }

        Result<RoleName> roleName = RoleName.Create(request.Name);
        if (roleName.IsFailure)
        {
            return Result.Failure<Guid>(roleName.Error);
        }

        RoleResponse? existingRole = await _roleReadRepository.GetByNameAsync(roleName.Value, cancellationToken);
        if (existingRole is not null && !existingRole.Id.Equals(role.Id.Value))
        {
            return Result.Failure<Guid>(RoleErrors.NameAlreadyExists);
        }

        role.UpdateName(roleName.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id.Value;
    }
}
