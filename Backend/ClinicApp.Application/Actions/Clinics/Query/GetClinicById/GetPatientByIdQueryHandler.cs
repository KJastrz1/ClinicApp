using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.Actions.Patients.Query.GetPatientById;
using ClinicApp.Application.Mappings;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Clinic.Responses;


namespace ClinicApp.Application.Actions.Clinics.Query.GetClinicById;

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
        Clinic? clinic = await _clinicRepository.GetByIdAsync(
            clinicId,
            cancellationToken);

        if (clinic is null)
        {
            return Result.Failure<ClinicResponse>(ClinicErrors.NotFound(clinicId));
        }

        ClinicResponse response = clinic.ToResponse();

        return response;
    }
}
