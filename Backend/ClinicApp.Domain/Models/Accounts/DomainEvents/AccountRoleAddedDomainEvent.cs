using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts.DomainEvents;

public record AccountRoleAddedDomainEvent(Guid AccountId, Guid RoleId) : DomainEvent(Guid.NewGuid());
