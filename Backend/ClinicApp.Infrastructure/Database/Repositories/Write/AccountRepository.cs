using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly WriteDbContext _writeContext;

    public AccountRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Account?> GetByIdAsync(AccountId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Accounts
            .Include(a => a.Roles)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Add(Account account)
    {
        _writeContext.Accounts.Add(account);
    }

    public void Update(Account account)
    {
        _writeContext.Accounts.Update(account);
    }

    public void Remove(Account account)
    {
        _writeContext.Accounts.Remove(account);
    }
}
