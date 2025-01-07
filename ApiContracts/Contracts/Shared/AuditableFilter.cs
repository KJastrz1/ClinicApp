namespace Shared.Contracts.Shared;

public record AuditableFilter
{
    public DateTime? CreatedOnUtcStart { get; set; }
    public DateTime? CreatedOnUtcEnd { get; set; }
    public DateTime? ModifiedOnUtcStart { get; set; }
    public DateTime? ModifiedOnUtcEnd { get; set; }
}
