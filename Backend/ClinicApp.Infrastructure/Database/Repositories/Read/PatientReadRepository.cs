using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Domain.Models.Accounts.ValueObjects;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class PatientReadRepository : IPatientReadRepository
{
    private readonly ReadDbContext _context;

    public PatientReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.AccountId.Equals(accountId), cancellationToken);
    }
}
