using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorAcademicTitleChangedDomainEvent(Guid DoctorId, string NewAcademicTitle) : DomainEvent(Guid.NewGuid());
