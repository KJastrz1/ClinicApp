using ClinicApp.Domain.Models.Patients.ValueObjects;
using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Requests;
using Shared.Contracts.Patient.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IPatientReadRepository
{
    Task<PatientResponse?> GetByIdAsync(PatientId patientId, CancellationToken cancellationToken);

    Task<PagedItems<PatientResponse>> GetByFilterAsync(PatientFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}
