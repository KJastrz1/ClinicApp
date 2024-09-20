using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountRoleRemovedDomainEvent(Guid AccountId, int RoleId) : DomainEvent(Guid.NewGuid());
