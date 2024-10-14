namespace Shared.Contracts.Doctor.Requests;

public record UpdateDoctorRequest(
    string? FirstName,
    string? LastName,
    string? MedicalLicenseNumber,
    List<string>? Specialties,
    string? Bio,
    string? AcademicTitle,
    Guid ClinicId
);
