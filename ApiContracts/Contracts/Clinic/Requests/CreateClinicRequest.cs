namespace Shared.Contracts.Clinic.Requests;

public sealed record CreateClinicRequest(
    string PhoneNumber,
    string Address,
    string City,
    string ZipCode
);
