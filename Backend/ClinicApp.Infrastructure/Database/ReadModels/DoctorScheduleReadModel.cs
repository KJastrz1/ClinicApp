using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class DoctorScheduleReadModel
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int ScheduleVisitDuration { get; set; }

    public ClinicReadModel Clinic { get; set; }

    public DoctorScheduleResponse MapToResponse()
    {
        return new DoctorScheduleResponse(
            Id: Id,
            Day: Day,
            StartTime: StartTime,
            EndTime: EndTime,
            VisitDuration: ScheduleVisitDuration,
            Clinic: Clinic.MapToResponse()
        );
    }
}
