using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using Shared.Contracts.Account.Responses;


namespace ClinicApp.Application.ReadRepositories;

public interface IAccountReadRepository
{
    Task<AccountResponse?> GetByIdAsync(AccountId id, CancellationToken cancellationToken);
    Task<AccountResponse?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<AccountResponse?> GetByEmailWithRolesAsync(Email email, CancellationToken cancellationToken);
}
