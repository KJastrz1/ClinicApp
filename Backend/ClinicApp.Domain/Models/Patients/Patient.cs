using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients.DomainEvents;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients;

public class Patient : User
{
    public SocialSecurityNumber SocialSecurityNumber { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }

    private Patient() { }

    private Patient(
        UserId id,
        FirstName firstName,
        LastName lastName,
        SocialSecurityNumber ssn,
        DateOfBirth dateOfBirth,
        AccountId? accountid) : base(id, firstName, lastName, UserType.Patient, accountid)
    {
        SocialSecurityNumber = ssn;
        DateOfBirth = dateOfBirth;
    }

    public static Patient Create(
        UserId id,
        FirstName firstName,
        LastName lastName,
        SocialSecurityNumber ssn,
        DateOfBirth dateOfBirth,
        AccountId? accountId)
    {
        var patient = new Patient(id, firstName, lastName, ssn, dateOfBirth, accountId);
        patient.RaiseDomainEvent(new PatientRegisteredDomainEvent(id.Value));
        return patient;
    }

    public Result<Patient> ChangeSocialSecurityNumber(SocialSecurityNumber newSsn)
    {
        if (SocialSecurityNumber.Equals(newSsn))
        {
            return Result.Success(this);
        }

        SocialSecurityNumber = newSsn;
        RaiseDomainEvent(new PatientSsnChangedDomainEvent(Id.Value, newSsn));
        return Result.Success(this);
    }

    public Result<Patient> ChangeDateOfBirth(DateOfBirth newDateOfBirth)
    {
        if (DateOfBirth.Equals(newDateOfBirth))
        {
            return Result.Success(this);
        }

        DateOfBirth = newDateOfBirth;
        RaiseDomainEvent(new PatientDateOfBirthChangedDomainEvent(Id, newDateOfBirth));
        return Result.Success(this);
    }
}
