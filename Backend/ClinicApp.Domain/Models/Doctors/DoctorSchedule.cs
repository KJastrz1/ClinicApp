using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors;

public class DoctorSchedule : Entity<DoctorScheduleId>
{
    public ScheduleDay Day { get; private set; }
    public StartTime StartTime { get; private set; }
    public EndTime EndTime { get; private set; }
    public ScheduleVisitDuration ScheduleVisitDuration { get; private set; }

    public ClinicId ClinicId { get; private set; }

    public Clinic Clinic { get; private set; }

    private DoctorSchedule() { }

    private DoctorSchedule(
        ScheduleDay day,
        StartTime startTime,
        EndTime endTime,
        ScheduleVisitDuration scheduleVisitDuration,
        Clinic clinic
    ) : base()
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        ScheduleVisitDuration = scheduleVisitDuration;
        Clinic = clinic;
        ClinicId = clinic.Id;
    }

    public static DoctorSchedule Create(
        ScheduleDay day,
        StartTime startTime,
        EndTime endTime,
        ScheduleVisitDuration scheduleVisitDuration,
        Clinic clinic
    )
    {
        var schedule = new DoctorSchedule(day, startTime, endTime, scheduleVisitDuration, clinic);

        return schedule;
    }
}
