using ClinicApp.Domain.Models.Accounts.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Account;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.ReadRepositories.Dapper;

public interface IAccountReadDapperRepository
{
    Task<AccountResponse?> GetByIdAsync(AccountId accountId, CancellationToken cancellationToken);

    Task<PagedItems<AccountResponse>> GetByFilterAsync(
        AccountFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
