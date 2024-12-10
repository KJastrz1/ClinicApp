using ClinicApp.Domain.Models.Clinics.ValueObjects;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IClinicReadRepository
{
    Task<ClinicResponse?> GetByIdAsync(ClinicId clinicId, CancellationToken cancellationToken);

    Task<PagedItems<ClinicResponse>> GetByFilterAsync(
        ClinicFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
