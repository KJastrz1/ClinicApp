using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Enums;
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
    private readonly IUserRoleService _userRoleService;

    public CreatePatientCommandHandler(
        IPatientRepository patientRepository,
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        IUserRoleService userRoleService
    )
    {
        _patientRepository = patientRepository;
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _userRoleService = userRoleService;
    }

    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        UserId userId = _userContext.Id;

        bool userProfileExists = await _userProfileRepository.ExistsAsync(userId, cancellationToken);
        if (userProfileExists)
        {
            return Result.Failure<Guid>(UserProfileErrors.AlreadyExists(userId));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<SocialSecurityNumber> ssnResult = SocialSecurityNumber.Create(request.SocialSecurityNumber);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(request.DateOfBirth);

        var patient = Patient.Create(
            userId,
            firstNameResult.Value,
            lastNameResult.Value,
            ssnResult.Value,
            dobResult.Value
        );

        _patientRepository.Add(patient);

        bool roleAssigned =
            await _userRoleService.AssignRoleToUserAsync(userId.Value, UserRoleEnum.Patient.ToString(),
                cancellationToken);
        if (!roleAssigned)
        {
            return Result.Failure<Guid>(UserProfileErrors.RoleAssignmentFailed(userId));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
