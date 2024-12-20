using ClinicApp.Domain.Models.Doctors.ValueObjects;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IDoctorReadRepository
{
    Task<DoctorResponse?> GetByIdAsync(DoctorId doctorId, CancellationToken cancellationToken);

    Task<PagedItems<DoctorResponse>> GetByFilterAsync(
        DoctorFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
