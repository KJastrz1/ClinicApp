using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Patients.Command.UpdatePatient;

internal sealed class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        PatientId patientId = PatientId.Create(request.PatientId).Value;
        Patient? patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient == null)
        {
            return Result.Failure<Guid>(PatientErrors.NotFound(patientId));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<SocialSecurityNumber> ssnResult = SocialSecurityNumber.Create(request.SocialSecurityNumber);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(request.DateOfBirth);

        patient.Update(
            firstNameResult.Value,
            lastNameResult.Value,
            ssnResult.Value,
            dobResult.Value
        );

        _patientRepository.Update(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
