using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class ScheduleErrors
{
    public static readonly Error EndTimeBeforeStartTime = new(
        "Schedule.EndTimeBeforeStartTime",
        "End time cannot be earlier than start time.");

    public static Error InvalidVisitDuration = new(
        "Schedule.InvalidVisitDuration",
        $"Visit durationMinutes must be between {ScheduleVisitDuration.MinDurationMinutes} and {ScheduleVisitDuration.MaxDurationMinutes} minutes.");

    public static readonly Error InvalidScheduleId = new(
        "Schedule.InvalidScheduleId",
        "Schedule Id must be greater than 0.");

    public static readonly Error DuplicateSchedule = new(
        "Schedule.Duplicate",
        "A schedule for this time already exists.");

    public static readonly Error EmptySchedulesList = new(
        "Schedule.EmptySchedulesList",
        "The list of schedules is empty."
    );
    
    public static readonly Error OverlappingSchedules = new(
        "Schedule.OverlappingSchedules",
        "Schedules cannot overlap with each other."
    );
}
