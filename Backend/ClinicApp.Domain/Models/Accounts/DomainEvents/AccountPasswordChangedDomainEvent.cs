using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountPasswordChangedDomainEvent(
    Guid UserId) : DomainEvent(Guid.NewGuid());
