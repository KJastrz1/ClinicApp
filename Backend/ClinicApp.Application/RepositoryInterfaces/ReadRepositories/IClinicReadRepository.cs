using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Shared;


namespace ClinicApp.Application.ReadRepositories;

public interface IClinicReadRepository
{
    Task<ClinicResponse?> GetByIdAsync(ClinicId clinicId, CancellationToken cancellationToken);

    Task<PagedItems<ClinicResponse>> GetByFilterAsync(
        ClinicFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
