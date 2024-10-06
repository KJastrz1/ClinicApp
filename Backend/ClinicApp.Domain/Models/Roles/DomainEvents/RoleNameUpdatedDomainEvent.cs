using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Roles.DomainEvents;

public record RoleNameUpdatedDomainEvent(Guid RoleId, string NewName) : DomainEvent(Guid.NewGuid());
