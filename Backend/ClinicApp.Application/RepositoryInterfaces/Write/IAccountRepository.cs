using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;

namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(AccountId id, CancellationToken cancellationToken);
    void Add(Account account);
    void Update(Account account);
    void Remove(Account account);
    Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<Account?> GetByEmailWithRolesAsync(Email email, CancellationToken cancellationToken);
}
