using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;

namespace ClinicApp.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(AccountId id, CancellationToken cancellationToken);
    void Add(Account account);
    void Update(Account account);
    void Remove(Account account);
}
