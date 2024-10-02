using System.Runtime.InteropServices.JavaScript;
using ClinicApp.Domain.Shared;
using ClinicApp.Domain.Models.Patients.ValueObjects;

namespace ClinicApp.Domain.Errors;

public static class PatientErrors
{
    public static Error NotFound(Guid id) => new(
        "Patients.NotFound",
        $"The patient with Id {id} was not found");

    public static Error EmptyId => new(
        "Patients.EmptyId",
        "Patients Id is empty.");

    public static Error PatientExists => new(
        "Patients.AccountExists",
        "Patient profile for this account already exists.");

    public static class SocialSecurityNumberErrors
    {
        public static readonly Error Empty = new(
            "SocialSecurityNumber.Empty",
            "Social security number is empty.");

        public static readonly Error InvalidLength = new(
            "SocialSecurityNumber.InvalidLength",
            $"Social security number must be {SocialSecurityNumber.Length} characters long (including hyphens).");

        public static readonly Error InvalidFormat = new(
            "SocialSecurityNumber.InvalidFormat",
            "Social security number must contain only digits and hyphens and must be in the format XXX-XX-XXXX.");
    }

    public static class DateOfBirthErrors
    {
        public static readonly Error InvalidFutureDate = new(
            "DateOfBirth.InvalidFutureDate",
            "Date of birth cannot be in the future.");

        public static readonly Error InvalidPastDate = new(
            "DateOfBirth.InvalidPastDate",
            $"Date of birth is too far in the past. The year must be {DateOfBirth.MinDate} or later.");
    }
}
