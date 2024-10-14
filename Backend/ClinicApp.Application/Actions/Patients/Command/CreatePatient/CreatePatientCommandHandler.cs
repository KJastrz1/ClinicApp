using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Patients.Command.CreatePatient;

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
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<SocialSecurityNumber> ssnResult = SocialSecurityNumber.Create(request.SocialSecurityNumber);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(request.DateOfBirth);

        var patient = Patient.Create(
            UserId.New(),
            firstNameResult.Value,
            lastNameResult.Value,
            ssnResult.Value,
            dobResult.Value,
            null
        );

        _patientRepository.Add(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
