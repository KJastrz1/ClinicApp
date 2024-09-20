using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountRegisteredDomainEvent(Guid AccountId) : DomainEvent(Guid.NewGuid());
