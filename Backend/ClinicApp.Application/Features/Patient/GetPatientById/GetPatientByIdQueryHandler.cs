using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Features.Patient.GetPatientById;

internal sealed class GetPatientByIdQueryHandler
    : IQueryHandler<GetPatientByIdQuery, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientResponse>> Handle(
        GetPatientByIdQuery request,
        CancellationToken cancellationToken)
    {
        PatientEntity? patient = await _patientRepository.GetByIdAsync(
            new PatientId(request.PatientId),
            cancellationToken);

        if (patient is null)
        {
            return Result.Failure<PatientResponse>(PatientErrors.NotFound(request.PatientId));
        }

        var response = new PatientResponse(
            patient.Id,
            patient.Email.Value,
            patient.FirstName.Value,
            patient.LastName.Value,
            patient.SocialSecurityNumber.Value,
            patient.DateOfBirth.Value);

        return response;
    }
}
