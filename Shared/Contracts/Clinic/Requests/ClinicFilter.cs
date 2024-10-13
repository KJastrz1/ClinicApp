namespace Shared.Contracts.Clinic.Requests;

public record ClinicFilter
{
    public string? PhoneNumber { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? ZipCode { get; init; }
}
