using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Shared;

namespace Frontend.Api;

public interface IDoctorsApi
{
    Task<PagedItems<DoctorBriefResponse>> GetDoctorsAsync(DoctorFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
