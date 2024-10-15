using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorScheduleAddedDomainEvent(
    Guid DoctorId,
    DayOfWeek DayValue,
    TimeSpan StartTimeValue,
    TimeSpan EndTimeValue) : DomainEvent(Guid.NewGuid());
