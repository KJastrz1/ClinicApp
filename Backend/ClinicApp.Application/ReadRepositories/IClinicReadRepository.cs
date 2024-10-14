using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Clinic.Requests;


namespace ClinicApp.Application.ReadRepositories;

public interface IClinicReadRepository
{
    Task<Clinic?> GetByIdAsync(ClinicId clinicId, CancellationToken cancellationToken);

    Task<PagedItems<Clinic>> GetByFilterAsync(
        ClinicFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
