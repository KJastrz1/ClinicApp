using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public sealed record DoctorDeletedDomainEvent(Guid DoctorId) : DomainEvent(Guid.NewGuid());
