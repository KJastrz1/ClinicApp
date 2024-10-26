using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class AccountReadRepository : IAccountReadRepository
{
    private readonly ReadDbContext _context;

    public AccountReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<AccountResponse?> GetByIdAsync(AccountId id, CancellationToken cancellationToken)
    {
        AccountReadModel? account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return account?.MapToResponse();
    }

    public async Task<AccountResponse?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        AccountReadModel? account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);

        return account?.MapToResponse();
    }

    public async Task<AccountResponse?> GetByEmailWithRolesAsync(Email email, CancellationToken cancellationToken)
    {
        AccountReadModel? account = await _context.Accounts
            .Include(a => a.Roles)
            .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);

        return account?.MapToResponse();
    }
}
