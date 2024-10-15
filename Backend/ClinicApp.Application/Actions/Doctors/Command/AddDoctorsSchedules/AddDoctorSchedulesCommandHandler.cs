using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Application.Actions.Doctors.Command.AddDoctorsSchedules;

internal sealed class AddDoctorSchedulesCommandHandler : ICommandHandler<AddDoctorSchedulesCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddDoctorSchedulesCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddDoctorSchedulesCommand request, CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);

        if (doctor == null)
        {
            return Result.Failure<Guid>(DoctorErrors.NotFound(DoctorId.Create(request.DoctorId).Value));
        }

        if (request.Schedules == null || request.Schedules.Count == 0)
        {
            return Result.Failure<Guid>(ScheduleErrors.EmptySchedulesList);
        }

        var schedules = new List<DoctorSchedule>();

        foreach (CreateDoctorScheduleRequest scheduleRequest in request.Schedules)
        {
            ScheduleDay scheduleDayResult = ScheduleDay.Create(scheduleRequest.Day).Value;
            StartTime startTimeResult = StartTime.Create(scheduleRequest.StartTime).Value;
            EndTime endTimeResult = EndTime.Create(scheduleRequest.EndTime).Value;
            VisitDuration visitDurationResult = VisitDuration.Create(scheduleRequest.VisitDuration).Value;

            var schedule = DoctorSchedule.Create(
                scheduleDayResult,
                startTimeResult,
                endTimeResult,
                visitDurationResult
            );
            schedules.Add(schedule);
        }

        foreach (DoctorSchedule schedule in schedules)
        {
            doctor.AddSchedule(schedule);
        }

        _doctorRepository.Update(doctor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return doctor.Id.Value;
    }
}
