using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Accounts.Command.AddRolesToAccount;

internal sealed class AddRolesToAccountCommandHandler : ICommandHandler<AddRolesToAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRolesToAccountCommandHandler(
        IAccountRepository accountRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddRolesToAccountCommand request, CancellationToken cancellationToken)
    {
        AccountId accountId = AccountId.Create(request.AccountId).Value;

        Account? account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
        if (account == null)
        {
            return Result.Failure<Guid>(AccountErrors.NotFound(accountId));
        }

        foreach (Guid roleIdValue in request.RolesIds)
        {
            RoleId roleId = RoleId.Create(roleIdValue).Value;
            Role? role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                return Result.Failure<Guid>(RoleErrors.NotFound(roleId));
            }

            account.AddRole(role);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id.Value;
    }
}
