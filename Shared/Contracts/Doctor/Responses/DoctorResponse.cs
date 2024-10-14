using System;

namespace Shared.Contracts.Doctor.Responses;

public record DoctorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string MedicalLicenseNumber,
    string[] Specialties, 
    string? Bio, 
    string? AcademicTitle, 
    Guid ClinicId
);
