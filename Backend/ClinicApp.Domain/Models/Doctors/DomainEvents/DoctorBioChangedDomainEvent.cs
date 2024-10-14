using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorBioChangedDomainEvent(Guid DoctorId, string NewBio) : DomainEvent(Guid.NewGuid());
