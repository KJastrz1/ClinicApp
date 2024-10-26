using Shared.Contracts.Clinic.Responses;

namespace Shared.Contracts.Doctor.Responses;

public sealed record DoctorScheduleResponse(
    int Id,
    DayOfWeek Day,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int VisitDuration,
    ClinicResponse Clinic
);
