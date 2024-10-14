using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorClinicChangedDomainEvent(Guid DoctorId, Guid newClinicId) : DomainEvent(Guid.NewGuid());
