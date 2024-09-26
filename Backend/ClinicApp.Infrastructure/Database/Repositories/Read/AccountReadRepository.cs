using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class AccountReadRepository : IAccountReadRepository
{
    private readonly ReadDbContext _context;

    public AccountReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public async Task<Account?> GetByEmailWithRolesAsync(Email email, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .Include(a => a.Roles)
            .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }
}
