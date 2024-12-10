using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Clinics.Query.GetClinics;

internal sealed class GetClinicsQueryHandler : IQueryHandler<GetClinicsQuery, PagedItems<ClinicResponse>>
{
    private readonly IClinicReadRepository _clinicReadRepository;

    public GetClinicsQueryHandler(IClinicReadRepository clinicReadRepository)
    {
        _clinicReadRepository = clinicReadRepository;
    }

    public async Task<Result<PagedItems<ClinicResponse>>> Handle(GetClinicsQuery request,
        CancellationToken cancellationToken)
    {
        PagedItems<ClinicResponse> items = await _clinicReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return Result.Success(items);
    }
}
