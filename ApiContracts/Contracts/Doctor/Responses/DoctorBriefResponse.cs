namespace Shared.Contracts.Doctor.Responses;

public record DoctorBriefResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string MedicalLicenseNumber,
    List<string> Specialties,
    string? AcademicTitle
);
