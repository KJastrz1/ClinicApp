namespace Shared.Contracts.Patient;

public record PatientFilter
{
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public bool? IsActivated { get; init; }
    public DateTime? CreatedOnUtcStart { get; init; }
    public DateTime? CreatedOnUtcEnd { get; init; }
    public DateTime? ModifiedOnUtcStart { get; init; }
    public DateTime? ModifiedOnUtcEnd { get; init; }
    public string? SocialSecurityNumber { get; init; }
    public DateTime? DateOfBirthStart { get; init; }
    public DateTime? DateOfBirthEnd { get; init; }
}
