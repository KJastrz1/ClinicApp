using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Admins.DomainEvents;

public sealed record AdminRegisteredDomainEvent(Guid Id, Guid AdminId) : DomainEvent(Id);
