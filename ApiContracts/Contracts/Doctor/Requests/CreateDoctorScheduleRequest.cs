namespace Shared.Contracts.Doctor.Requests;

public sealed record CreateDoctorScheduleRequest(
    DayOfWeek Day,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int VisitDuration,
    Guid ClinicId
);
