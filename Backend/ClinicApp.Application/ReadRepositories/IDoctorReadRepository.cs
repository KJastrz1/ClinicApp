using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Application.ReadRepositories;

public interface IDoctorReadRepository
{
    Task<Doctor?> GetByIdAsync(DoctorId doctorId, CancellationToken cancellationToken);

    Task<PagedItems<Doctor>> GetByFilterAsync(
        DoctorFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
