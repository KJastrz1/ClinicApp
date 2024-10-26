using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.UseCases.Accounts.Query.LoginAccount;

internal sealed class LoginAccountQueryHandler : IQueryHandler<LoginAccountQuery, LoginResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginAccountQueryHandler(
        IAccountRepository accountRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
    {
        Email email = Email.Create(request.Username).Value;

        Account? account =
            await _accountRepository.GetByEmailWithRolesAsync(email, cancellationToken);

        if (account is null)
        {
            return Result.Failure<LoginResponse>(AccountErrors.LoginErrors.InvalidCredentials);
        }

        if (!_passwordHasher.Verify(request.Password, account.PasswordHash.Value))
        {
            return Result.Failure<LoginResponse>(AccountErrors.LoginErrors.InvalidCredentials);
        }

        string token = _jwtProvider.Generate(account);

        var loginResponse = new LoginResponse(token, account.MapToResponse());

        return Result.Success(loginResponse);
    }
}
