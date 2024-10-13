using ClinicApp.Domain.Models.Patients.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.ReadRepositories.Dapper;

public interface IPatientReadDapperRepository
{
    Task<PatientResponse?> GetByIdAsync(PatientId patientId, CancellationToken cancellationToken);

    Task<PagedItems<PatientResponse>> GetByFilterAsync(
        PatientFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
