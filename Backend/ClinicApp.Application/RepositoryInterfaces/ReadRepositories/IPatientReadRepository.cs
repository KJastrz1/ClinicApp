using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Patient;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.ReadRepositories;

public interface IPatientReadRepository
{
    Task<PatientResponse?> GetByIdAsync(PatientId patientId, CancellationToken cancellationToken);

    Task<PagedItems<PatientResponse>> GetByFilterAsync(PatientFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}
