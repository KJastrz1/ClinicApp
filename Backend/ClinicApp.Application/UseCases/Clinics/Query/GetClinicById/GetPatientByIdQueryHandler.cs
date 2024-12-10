using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Application.UseCases.Clinics.Query.GetClinicById;

internal sealed class GetClinicByIdQueryHandler
    : IQueryHandler<GetClinicByIdQuery, ClinicResponse>
{
    private readonly IClinicReadRepository _clinicRepository;

    public GetClinicByIdQueryHandler(IClinicReadRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public async Task<Result<ClinicResponse>> Handle(
        GetClinicByIdQuery request,
        CancellationToken cancellationToken)
    {
        ClinicId clinicId = ClinicId.Create(request.ClinicId).Value;
        ClinicResponse? clinic = await _clinicRepository.GetByIdAsync(
            clinicId,
            cancellationToken);

        if (clinic is null)
        {
            return Result.Failure<ClinicResponse>(ClinicErrors.NotFound(clinicId));
        }

        return clinic;
    }
}
