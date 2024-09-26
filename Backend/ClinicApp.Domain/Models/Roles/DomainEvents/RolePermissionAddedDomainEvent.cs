using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Roles.DomainEvents;

public record RolePermissionAddedDomainEvent(Guid RoleId, int PermissionId) : DomainEvent(Guid.NewGuid());

