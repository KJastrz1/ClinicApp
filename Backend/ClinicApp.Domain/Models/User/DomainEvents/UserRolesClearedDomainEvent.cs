using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User.DomainEvents;

public sealed record UserRolesClearedDomainEvent(Guid Id, Guid UserId) : DomainEvent(Id);
