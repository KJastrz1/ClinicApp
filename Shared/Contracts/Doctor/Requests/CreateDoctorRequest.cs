namespace Shared.Contracts.Doctor.Requests;

public sealed record CreateDoctorRequest(
    string FirstName,
    string LastName,
    string MedicalLicenseNumber,
    List<string>? Specialties,
    string? Bio,
    string? AcademicTitle,
    Guid AccountId,
    Guid ClinicId
);
