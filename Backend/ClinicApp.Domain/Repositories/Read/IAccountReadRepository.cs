using ClinicApp.Domain.Models.Accounts.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Auth;

namespace ClinicApp.Domain.Repositories.Read;

public interface IAccountReadRepository
{
    Task<AccountResponse?> GetByIdAsync(AccountId accountId, CancellationToken cancellationToken);

    Task<PagedResult<AccountResponse>> GetByFilterAsync(
        AccountFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
