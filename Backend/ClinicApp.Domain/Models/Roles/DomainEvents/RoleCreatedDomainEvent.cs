using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Roles.DomainEvents;

public record RoleCreatedDomainEvent(Guid RoleId) : DomainEvent(Guid.NewGuid());
