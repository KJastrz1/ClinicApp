namespace ClinicApp.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}
