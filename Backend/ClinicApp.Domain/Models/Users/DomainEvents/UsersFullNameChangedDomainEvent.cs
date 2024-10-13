using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Users.DomainEvents;

public sealed record UsersFullNameChangedDomainEvent(Guid UserId, string NewFullName) : DomainEvent(Guid.NewGuid());
