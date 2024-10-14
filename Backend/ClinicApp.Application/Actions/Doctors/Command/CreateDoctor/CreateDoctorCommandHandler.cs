using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;


namespace ClinicApp.Application.Actions.Doctors.Command.CreateDoctor;

internal sealed class CreateDoctorCommandHandler : ICommandHandler<CreateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IClinicRepository _clinicRepository;

    public CreateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        IClinicRepository clinicRepository)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _clinicRepository = clinicRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
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

        Account? account = null;
        if (request.AccountId != null && request.AccountId != Guid.Empty)
        {
            AccountId accountId = AccountId.Create(request.AccountId.Value).Value;
            account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
        }

        Clinic? clinic = null;
        if (request.ClinicId != null && request.ClinicId != Guid.Empty)
        {
            ClinicId clinicId = ClinicId.Create(request.ClinicId.Value).Value;
            clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken);
        }

        var doctor = Doctor.Create(
            UserId.New(),
            firstNameResult.Value,
            lastNameResult.Value,
            licenseNumberResult.Value,
            specialties,
            bio,
            academicTitle,
            account
        );

        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return doctor.Id.Value;
    }
}
