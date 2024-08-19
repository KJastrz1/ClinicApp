using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User.DomainEvents;

public sealed record UserEmailChangedDomainEvent(
    Guid Id,
    Guid UserId,
    string OldEmail,
    string NewEmail) : DomainEvent(Id);
