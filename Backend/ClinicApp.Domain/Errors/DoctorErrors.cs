using ClinicApp.Domain.Shared;
using ClinicApp.Domain.Models.Doctors.ValueObjects;

namespace ClinicApp.Domain.Errors;

public static class DoctorErrors
{
    public static Error NotFound(DoctorId id) => new(
        "Doctors.NotFound",
        $"The doctor with Id {id.Value} was not found.");

    public static Error EmptyId => new(
        "Doctors.EmptyId",
        "Doctor Id is empty.");

    public static Error DoctorExists => new(
        "Doctors.AccountExists",
        "Doctor profile for this account already exists.");

    public static class MedicalLicenseNumberErrors
    {
        public static readonly Error Empty = new(
            "MedicalLicenseNumber.Empty",
            "Medical license number is empty.");

        public static readonly Error TooLong = new(
            "MedicalLicenseNumber.TooLong",
            $"Medical license number must be at most {MedicalLicenseNumber.MaxLength} characters long.");
    }

    public static class SpecialtyErrors
    {
        public static readonly Error Empty = new(
            "Specialty.Empty",
            "Specialties list cannot be empty if provided.");

        public static readonly Error TooLong = new(
            "Specialty.TooLong",
            $"Specialty must be at most {Specialty.MaxLength} characters long.");
    }

    public static class BioErrors
    {
        public static readonly Error TooLong = new(
            "Bio.TooLong",
            $"Bio must be at most {Bio.MaxLength} characters long.");
    }

    public static class AcademicTitleErrors
    {
        public static readonly Error Empty = new(
            "AcademicTitle.Empty",
            "Academic title is empty.");

        public static readonly Error TooLong = new(
            "AcademicTitle.TooLong",
            $"Academic title must be at most {AcademicTitle.MaxLength} characters long.");
    }
}
