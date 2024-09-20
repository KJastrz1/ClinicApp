using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountActivatedDomainEvent(Guid AccountId) : DomainEvent(Guid.NewGuid());
