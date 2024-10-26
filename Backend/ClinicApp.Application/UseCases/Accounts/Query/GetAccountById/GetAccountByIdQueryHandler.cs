using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.UseCases.Accounts.Query.GetAccountById;

internal sealed class GetAccountByIdQueryHandler
    : IQueryHandler<GetAccountByIdQuery, AccountResponse>
{
    private readonly IAccountReadRepository _accountReadRepository;

    public GetAccountByIdQueryHandler(IAccountReadRepository accountReadRepository)
    {
        _accountReadRepository = accountReadRepository;
    }

    public async Task<Result<AccountResponse>> Handle(
        GetAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        AccountResponse? accountResponse = await _accountReadRepository.GetByIdAsync(
            AccountId.Create(request.Id).Value,
            cancellationToken);

        if (accountResponse is null)
        {
            return Result.Failure<AccountResponse>(AccountErrors.NotFound(request.Id));
        }

        return Result.Success(accountResponse);
    }
}
