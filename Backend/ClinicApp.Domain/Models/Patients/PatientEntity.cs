using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Patients.DomainEvents;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.User;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Models.UserBase;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients;

public class PatientEntity : UserBase<PatientId>
{
    public SocialSecurityNumber SocialSecurityNumber { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }


    private PatientBase(
        PatientId id,
        FirstName firstName,
        LastName lastName,
        Email email,
        SocialSecurityNumber ssn,
        DateOfBirth dateOfBirth) : base(id, email, firstName, lastName, UserType.Patient)
    {
        SocialSecurityNumber = ssn;
        DateOfBirth = dateOfBirth;
        RaiseDomainEvent(new PatientRegisteredDomainEvent(Id.Value));
    }


    private PatientBase() { }


    public static PatientBase Create(
        PatientId id,
        FirstName firstName,
        LastName lastName,
        Email email,
        SocialSecurityNumber ssn,
        DateOfBirth dateOfBirth)
    {
        var patient = new PatientBase(id, firstName, lastName, email, ssn, dateOfBirth);
        patient.RaiseDomainEvent(new PatientRegisteredDomainEvent(id.Value));
        return patient;
    }

    public Result<PatientBase> ChangeSocialSecurityNumber(SocialSecurityNumber newSsn)
    {
        if (SocialSecurityNumber.Equals(newSsn))
        {
            return Result.Success(this);
        }

        SocialSecurityNumber = newSsn;
        RaiseDomainEvent(new PatientSsnChangedDomainEvent(Id.Value, newSsn));
        return Result.Success(this);
    }

    public Result<PatientBase> ChangeDateOfBirth(DateOfBirth newDateOfBirth)
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
