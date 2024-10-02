using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Patients.Query.GetPatientById;

internal sealed class GetPatientByIdQueryHandler
    : IQueryHandler<GetPatientByIdQuery, PatientResponse>
{
    private readonly IPatientReadDapperRepository _patientRepository;

    public GetPatientByIdQueryHandler(IPatientReadDapperRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientResponse>> Handle(
        GetPatientByIdQuery request,
        CancellationToken cancellationToken)
    {
        PatientResponse? response = await _patientRepository.GetByIdAsync(
            PatientId.Create(request.PatientId).Value,
            cancellationToken);

        if (response is null)
        {
            return Result.Failure<PatientResponse>(PatientErrors.NotFound(request.PatientId));
        }

        return response;
    }
}
