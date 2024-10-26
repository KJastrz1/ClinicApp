namespace Shared.Contracts.Shared;

public record AuditableFilter
{
    public DateTime? CreatedOnUtcStart { get; init; }
    public DateTime? CreatedOnUtcEnd { get; init; }
    public DateTime? ModifiedOnUtcStart { get; init; }
    public DateTime? ModifiedOnUtcEnd { get; init; }
}
