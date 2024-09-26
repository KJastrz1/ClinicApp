using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountRoleRemovedDomainEvent(Guid AccountId, Guid RoleId) : DomainEvent(Guid.NewGuid());
