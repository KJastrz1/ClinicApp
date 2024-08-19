using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User.DomainEvents;

public sealed record UserRoleAddedDomainEvent(Guid Id, Guid UserId, int RoleId) : DomainEvent(Id);
