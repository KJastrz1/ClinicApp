using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Command.CreateDoctor;

internal sealed class CreateDoctorCommandHandler : ICommandHandler<CreateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClinicRepository _clinicRepository;
    private readonly IUserContext _userContext;

    public CreateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IClinicRepository clinicRepository,
        IUserContext userContext)
    {
        _doctorRepository = doctorRepository;
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
        _clinicRepository = clinicRepository;
        _userContext = userContext;
    }

    public async Task<Result<Guid>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        Result<UserId> userIdResult = UserId.Create(Guid.Parse(_userContext.Id));

        bool userProfileExists = await _userProfileRepository.ExistsAsync(userIdResult.Value, cancellationToken);
        if (userProfileExists)
        {
            return Result.Failure<Guid>(UserProfileErrors.AlreadyExists(userIdResult.Value));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<MedicalLicenseNumber> licenseNumberResult = MedicalLicenseNumber.Create(request.MedicalLicenseNumber);

        var specialties = new List<Specialty>();

        if (request.Specialties != null)
        {
            foreach (string specialty in request.Specialties)
            {
                Specialty specialtyResult = Specialty.Create(specialty).Value;
                specialties.Add(specialtyResult);
            }
        }

        Bio? bio = null;
        if (!string.IsNullOrWhiteSpace(request.Bio))
        {
            bio = Bio.Create(request.Bio).Value;
        }

        AcademicTitle? academicTitle = null;
        if (!string.IsNullOrWhiteSpace(request.AcademicTitle))
        {
            academicTitle = AcademicTitle.Create(request.AcademicTitle).Value;
        }

        var doctor = Doctor.Create(
            userIdResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            licenseNumberResult.Value,
            specialties,
            bio,
            academicTitle);

        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return doctor.Id.Value;
    }
}
