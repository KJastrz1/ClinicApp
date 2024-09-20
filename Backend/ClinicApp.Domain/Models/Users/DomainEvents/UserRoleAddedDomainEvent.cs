using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Users.DomainEvents;

public sealed record UserRoleAddedDomainEvent(Guid Id, Guid UserId, int RoleId) : DomainEvent(Id);
