namespace Shared.Contracts.Clinic.Requests;

public record UpdateClinicRequest(
    string? PhoneNumber,
    string? Address,
    string? City,
    string? ZipCode
);
