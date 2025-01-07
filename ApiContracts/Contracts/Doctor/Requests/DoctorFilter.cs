using Shared.Contracts.Shared;

namespace Shared.Contracts.Doctor.Requests;

public record DoctorFilter : AuditableFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Specialty { get; set; }
}
