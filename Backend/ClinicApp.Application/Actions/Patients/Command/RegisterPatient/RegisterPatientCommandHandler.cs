using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Patients.Command.RegisterPatient;

internal sealed class RegisterPatientCommandHandler :
    ICommandHandler<RegisterPatientCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterPatientCommandHandler(
        IAccountRepository accountRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _accountRepository = accountRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        string passwordHash = _passwordHasher.Hash(request.Password);
        var accountId = AccountId.New();
        Result<Email> emailResult = Email.Create(request.Email);
        Result<PasswordHash> passwordHashResult = PasswordHash.Create(passwordHash);


        var account = Account.Create(
            accountId,
            emailResult.Value,
            passwordHashResult.Value);

        _accountRepository.Add(account);

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
            accountId
        );

        _patientRepository.Add(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.Value;
    }
}
