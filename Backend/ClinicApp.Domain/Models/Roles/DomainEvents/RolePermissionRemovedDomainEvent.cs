using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Roles.DomainEvents;

public record RolePermissionRemovedDomainEvent(Guid RoleId, int PermissionId) : DomainEvent(Guid.NewGuid());
