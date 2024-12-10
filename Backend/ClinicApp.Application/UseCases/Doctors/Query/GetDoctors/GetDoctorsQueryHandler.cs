using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Query.GetDoctors;

internal sealed class GetDoctorsQueryHandler : IQueryHandler<GetDoctorsQuery, PagedItems<DoctorResponse>>
{
    private readonly IDoctorReadRepository _doctorReadRepository;

    public GetDoctorsQueryHandler(IDoctorReadRepository doctorReadRepository)
    {
        _doctorReadRepository = doctorReadRepository;
    }

    public async Task<Result<PagedItems<DoctorResponse>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        PagedItems<DoctorResponse> items = await _doctorReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);
     

        return Result.Success(items);
    }
}
