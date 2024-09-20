using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountRolesClearedDomainEvent(Guid AccountId) : DomainEvent(Guid.NewGuid());
