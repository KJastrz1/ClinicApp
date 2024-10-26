using System;

namespace Shared.Contracts.Doctor.Responses;

public record DoctorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string MedicalLicenseNumber,
    List<string> Specialties,
    string? Bio,
    string? AcademicTitle
);
