using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.Mappings;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Shared;
using Shared.Contracts;
using Shared.Contracts.Clinic;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Application.Actions.Clinics.Query.GetClinics;

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
        PagedItems<Clinic> items = await _clinicReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);
        
        var responseItems = new PagedItems<ClinicResponse>
        {
            Items = items.Items.Select(clinic => clinic.ToResponse()).ToList(),
            TotalCount = items.TotalCount,
            PageSize = items.PageSize,
            CurrentPage = items.CurrentPage };

        return Result.Success(responseItems);
    }
}
