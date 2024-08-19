using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;


namespace ClinicApp.Application.Features.Patient.CreatePatient;

internal sealed class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var validationResults = new List<Result>();

        Result<Email> emailResult = Email.Create(request.Email);
        validationResults.Add(emailResult);

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        validationResults.Add(firstNameResult);

        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        validationResults.Add(lastNameResult);

        Result<SocialSecurityNumber> ssnResult = SocialSecurityNumber.Create(request.SocialSecurityNumber);
        validationResults.Add(ssnResult);

        Result<DateOfBirth> dobResult = DateOfBirth.Create(request.DateOfBirth);
        validationResults.Add(dobResult);

        var errors = validationResults.Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToList();

        // if (errors.Any())
        // {
        //     return Result.Failure<Guid>(errors);
        // }

        if (!await _patientRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(UserBaseErrors.EmailErrors.EmailAlreadyInUse);
        }

        var patient = PatientBase.Create(
            Guid.NewGuid(),
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            ssnResult.Value,
            dobResult.Value);

        _patientRepository.Add(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
