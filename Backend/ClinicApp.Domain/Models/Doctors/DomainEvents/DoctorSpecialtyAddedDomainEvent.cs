using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorSpecialtyAddedDomainEvent(Guid DoctorId, string Specialty) : DomainEvent(Guid.NewGuid());
