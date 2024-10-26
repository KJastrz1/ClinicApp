using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Appointments.Command.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IClinicRepository _clinicRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IClinicRepository clinicRepository,
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _clinicRepository = clinicRepository;
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is null)
        {
            return Result.Failure<Guid>(DoctorErrors.NotFound(doctorId));
        }

        PatientId patientId = PatientId.Create(request.PatientId).Value;
        Patient? patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null)
        {
            return Result.Failure<Guid>(PatientErrors.NotFound(patientId));
        }

        ClinicId clinicId = ClinicId.Create(request.ClinicId).Value;
        Clinic? clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken);
        if (clinic is null)
        {
            return Result.Failure<Guid>(ClinicErrors.NotFound(clinicId));
        }

        DoctorSchedule? scheduleForDay = doctor.Schedules
            .FirstOrDefault(schedule =>
                schedule.Day.Value == request.StartDateTime.DayOfWeek
                && schedule.StartTime.Value <= request.StartDateTime.TimeOfDay
                && schedule.EndTime.Value >= request.StartDateTime.TimeOfDay +
                TimeSpan.FromMinutes(schedule.ScheduleVisitDuration.Value)
                && (int)(request.StartDateTime.TimeOfDay - schedule.StartTime.Value).TotalMinutes %
                schedule.ScheduleVisitDuration.Value == 0);

        if (scheduleForDay is null)
        {
            return Result.Failure<Guid>(AppointmentErrors.AppointmentDateNotAvailable);
        }

        AppointmentStartDateTime startDateTime = AppointmentStartDateTime.Create(request.StartDateTime).Value;
        Appointment? existingAppointment =
            await _appointmentRepository.GetByStartDate(startDateTime, cancellationToken);

        if (existingAppointment is not null)
        {
            return Result.Failure<Guid>(AppointmentErrors.AppointmentDateTaken);
        }

        AppointmentDurationMinutes appointmentDurationMinutes =
            AppointmentDurationMinutes.Create(scheduleForDay.ScheduleVisitDuration.Value).Value;

        AppointmentNotes? notes = null;
        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            notes = AppointmentNotes.Create(request.Notes).Value;
        }

        var appointment = Appointment.Create(
            AppointmentId.New(),
            doctorId,
            patientId,
            clinicId,
            startDateTime,
            appointmentDurationMinutes,
            notes
        );


        _appointmentRepository.Add(appointment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id.Value;
    }
}
