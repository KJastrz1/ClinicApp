using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors;

public class DoctorSchedule : Entity<DoctorScheduleId>
{
    public ScheduleDay Day { get; private set; }
    public StartTime StartTime { get; private set; }
    public EndTime EndTime { get; private set; }
    public VisitDuration VisitDuration { get; private set; }

    private DoctorSchedule() { }

    private DoctorSchedule(ScheduleDay day, StartTime startTime, EndTime endTime, VisitDuration visitDuration
    ) : base()
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        VisitDuration = visitDuration;
    }

    public static DoctorSchedule Create(
        ScheduleDay day,
        StartTime startTime,
        EndTime endTime,
        VisitDuration visitDuration
    )
    {
        var schedule = new DoctorSchedule(day, startTime, endTime, visitDuration);

        return schedule;
    }
}
