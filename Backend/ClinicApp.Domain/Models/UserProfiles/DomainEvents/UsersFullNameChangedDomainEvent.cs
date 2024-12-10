using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.UserProfiles.DomainEvents;

public sealed record UsersFullNameChangedDomainEvent(Guid UserId, string NewFullName) : DomainEvent(Guid.NewGuid());
