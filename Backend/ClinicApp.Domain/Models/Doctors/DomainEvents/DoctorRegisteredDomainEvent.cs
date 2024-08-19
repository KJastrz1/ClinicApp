using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public sealed record DoctorRegisteredDomainEvent(Guid Id, Guid DoctorId) : DomainEvent(Id);
