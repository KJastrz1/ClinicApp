using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Application.UseCases.Doctors.Command.AddDoctorsSchedules;

internal sealed class AddDoctorSchedulesCommandHandler : ICommandHandler<AddDoctorSchedulesCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddDoctorSchedulesCommandHandler(
        IDoctorRepository doctorRepository,
        IClinicRepository clinicRepository,
        IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddDoctorSchedulesCommand request, CancellationToken cancellationToken)
    {
        if (request.Schedules == null || request.Schedules.Count == 0)
        {
            return Result.Failure<Guid>(ScheduleErrors.EmptySchedulesList);
        }

        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);

        if (doctor == null)
        {
            return Result.Failure<Guid>(DoctorErrors.NotFound(DoctorId.Create(request.DoctorId).Value));
        }

        var schedules = new List<DoctorSchedule>();

        foreach (CreateDoctorScheduleRequest scheduleRequest in request.Schedules)
        {
            ScheduleDay scheduleDayResult = ScheduleDay.Create(scheduleRequest.Day).Value;
            StartTime startTimeResult = StartTime.Create(scheduleRequest.StartTime).Value;
            EndTime endTimeResult = EndTime.Create(scheduleRequest.EndTime).Value;
            ScheduleVisitDuration scheduleVisitDurationResult =
                ScheduleVisitDuration.Create(scheduleRequest.VisitDuration).Value;

            ClinicId clinicId = ClinicId.Create(scheduleRequest.ClinicId).Value;

            Clinic? clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken);
            
            if (clinic is null)
            {
                return Result.Failure<Guid>(ClinicErrors.NotFound(clinicId));
            }

            var schedule = DoctorSchedule.Create(
                scheduleDayResult,
                startTimeResult,
                endTimeResult,
                scheduleVisitDurationResult,
                clinic
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
