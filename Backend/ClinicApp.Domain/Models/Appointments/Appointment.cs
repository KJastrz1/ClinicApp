using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Appointments.DomainEvents;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments;

public class Appointment : AggregateRoot<AppointmentId>, IAuditableEntity
{
    public AppointmentStartDateTime StartDateTime { get; private set; }
    public AppointmentDurationMinutes DurationMinutes { get; private set; }
    public AppointmentNotes? Notes { get; private set; }
    public AppointmentStatusEnum Status { get; private set; }
    public bool IsReminderSent { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }

    public UserId DoctorId { get; private set; }
    public UserId PatientId { get; private set; }
    public ClinicId ClinicId { get; private set; }
    public Doctor Doctor { get; private set; }
    public Patient Patient { get; private set; }
    public Clinic Clinic { get; private set; }

    private Appointment() { }

    private Appointment(
        AppointmentId id,
        DoctorId doctorId,
        PatientId patientId,
        ClinicId clinicId,
        AppointmentStartDateTime startDateTime,
        AppointmentDurationMinutes durationMinutes,
        AppointmentNotes? notes,
        AppointmentStatusEnum status
    ) : base(id)
    {
        DoctorId = UserId.Create(doctorId.Value).Value;
        PatientId = UserId.Create(patientId.Value).Value;
        ClinicId = clinicId;
        StartDateTime = startDateTime;
        DurationMinutes = durationMinutes;
        Notes = notes;
        Status = status;
    }

    public static Appointment Create(
        AppointmentId id,
        DoctorId doctorId,
        PatientId patientId,
        ClinicId clinicId,
        AppointmentStartDateTime appointmentDateTime,
        AppointmentDurationMinutes appointmentDurationMinutes,
        AppointmentNotes? notes
    )
    {
        var appointment = new Appointment(id, doctorId, patientId, clinicId, appointmentDateTime,
            appointmentDurationMinutes,
            notes, AppointmentStatusEnum.Reserved);
        appointment.RaiseDomainEvent(new AppointmentCreatedDomainEvent(id.Value));
        return appointment;
    }

    public Result<Appointment> UpdateNotes(AppointmentNotes notes)
    {
        if (Notes != null && Notes.Equals(notes))
        {
            return Result.Success(this);
        }

        Notes = notes;
        RaiseDomainEvent(new AppointmentNotesUpdatedDomainEvent(Id.Value, notes.Value));
        return Result.Success(this);
    }

    public void UpdateStatus(AppointmentStatusEnum newStatus)
    {
        if (Status != newStatus)
        {
            Status = newStatus;
            RaiseDomainEvent(new AppointmentStatusUpdatedDomainEvent(Id.Value, newStatus));
        }
    }

    public void SendReminder()
    {
        if (!IsReminderSent)
        {
            IsReminderSent = true;
            RaiseDomainEvent(new AppointmentReminderSentDomainEvent(Id.Value));
        }
    }

    public void Delete()
    {
        RaiseDomainEvent(new AppointmentDeletedDomainEvent(Id.Value));
    }
}
