using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients;

namespace ClinicApp.Application.ReadRepositories;

public interface IPatientReadRepository
{
    Task<Patient?> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken);
}
