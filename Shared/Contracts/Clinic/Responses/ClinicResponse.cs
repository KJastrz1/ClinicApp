namespace Shared.Contracts.Clinic.Responses;

public record ClinicResponse(
    Guid Id,
    string PhoneNumber,
    string Address,
    string City,
    string ZipCode
);
