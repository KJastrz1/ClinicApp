using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountDeactivatedDomainEvent(Guid AccountId) : DomainEvent(Guid.NewGuid());
