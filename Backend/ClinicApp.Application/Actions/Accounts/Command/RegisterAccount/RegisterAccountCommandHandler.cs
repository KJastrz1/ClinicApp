using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Accounts.Command.RegisterAccount;

internal sealed class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);

        string passwordHash = _passwordHasher.Hash(request.Password);

        var account = Account.Create(
            AccountId.New(),
            emailResult.Value,
            PasswordHash.Create(passwordHash).Value
        );

        _accountRepository.Add(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id.Value;
    }
}
