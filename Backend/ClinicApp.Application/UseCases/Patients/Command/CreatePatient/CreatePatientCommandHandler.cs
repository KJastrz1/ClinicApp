using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Patients.Command.CreatePatient;

internal sealed class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreatePatientCommandHandler(
        IPatientRepository patientRepository,
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext
    )
    {
        _patientRepository = patientRepository;
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        Result<UserId> userIdResult = UserId.Create(Guid.Parse(_userContext.Id));

        bool userProfileExists = await _userProfileRepository.ExistsAsync(userIdResult.Value, cancellationToken);
        if (userProfileExists)
        {
            return Result.Failure<Guid>(UserProfileErrors.AlreadyExists(userIdResult.Value));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<SocialSecurityNumber> ssnResult = SocialSecurityNumber.Create(request.SocialSecurityNumber);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(request.DateOfBirth);

        var patient = Patient.Create(
            userIdResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            ssnResult.Value,
            dobResult.Value
        );

        _patientRepository.Add(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
