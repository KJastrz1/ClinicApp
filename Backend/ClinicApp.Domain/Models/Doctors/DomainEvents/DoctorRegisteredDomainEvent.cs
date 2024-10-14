using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public sealed record DoctorRegisteredDomainEvent(Guid DoctorId) : DomainEvent(Guid.NewGuid());
