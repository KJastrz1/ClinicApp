using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Mappings;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.Actions.Accounts.Query.LoginAccount;

internal sealed class LoginAccountQueryHandler : IQueryHandler<LoginAccountQuery, LoginResponse>
{
    private readonly IAccountReadRepository _accountReadRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginAccountQueryHandler(
        IAccountReadRepository accountReadRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _accountReadRepository = accountReadRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
    {
        Email email = Email.Create(request.Username).Value;

        Account? account =
            await _accountReadRepository.GetByEmailWithRolesAsync(email, cancellationToken);

        if (account is null)
        {
            return Result.Failure<LoginResponse>(AccountErrors.LoginErrors.InvalidCredentials);
        }

        if (!_passwordHasher.Verify(request.Password, account.PasswordHash.Value))
        {
            return Result.Failure<LoginResponse>(AccountErrors.LoginErrors.InvalidCredentials);
        }

        string token = _jwtProvider.Generate(account);

        var loginResponse = new LoginResponse(token, account.ToResponse());

        return Result.Success(loginResponse);
    }
}
