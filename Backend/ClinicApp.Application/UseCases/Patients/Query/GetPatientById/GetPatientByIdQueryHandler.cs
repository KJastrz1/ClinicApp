using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Responses;

namespace ClinicApp.Application.UseCases.Patients.Query.GetPatientById;

internal sealed class GetPatientByIdQueryHandler
    : IQueryHandler<GetPatientByIdQuery, PatientResponse>
{
    private readonly IPatientReadRepository _patientRepository;

    public GetPatientByIdQueryHandler(IPatientReadRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientResponse>> Handle(
        GetPatientByIdQuery request,
        CancellationToken cancellationToken)
    {
        PatientId patientId = PatientId.Create(request.PatientId).Value;
        PatientResponse? response = await _patientRepository.GetByIdAsync(
            patientId,
            cancellationToken);

        if (response is null)
        {
            return Result.Failure<PatientResponse>(PatientErrors.NotFound(patientId));
        }

        return response;
    }
}
