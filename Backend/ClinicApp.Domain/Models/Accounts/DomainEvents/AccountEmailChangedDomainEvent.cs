using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public sealed record AccountEmailChangedDomainEvent(
    Guid UserId,
    string OldEmail,
    string NewEmail) : DomainEvent(Guid.NewGuid());
