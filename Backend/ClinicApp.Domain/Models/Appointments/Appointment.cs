using ClinicApp.Domain.Models.Appointments.DomainEvents;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments;

public class Appointment : AggregateRoot<AppointmentId>, IAuditableEntity
{
    public ClinicId ClinicId { get; private set; }
    public DoctorId DoctorId { get; private set; }
    public PatientId PatientId { get; private set; }
    public AppointmentDateTime AppointmentDate { get; private set; }
    public AppointmentDateTime EndDate { get; private set; }
    public AppointmentNotes Notes { get; private set; }
    public AppointmentRoom Room { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public Appointment(
        AppointmentId id,
        ClinicId clinicId,
        DoctorId doctorId,
        PatientId patientId,
        AppointmentDateTime appointmentDate,
        AppointmentDateTime endDate,
        AppointmentRoom room) : base(id)
    {
        ClinicId = clinicId;
        DoctorId = doctorId;
        PatientId = patientId;
        AppointmentDate = appointmentDate;
        EndDate = endDate;
        Room = room;
        Notes = AppointmentNotes.Empty;
        CreatedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new AppointmentCreatedDomainEvent(Id.Value));
    }

    public void UpdateNotes(AppointmentNotes notes)
    {
        Notes = notes;
        ModifiedOnUtc = DateTime.UtcNow;
    }

 
}
