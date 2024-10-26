namespace Shared.Contracts.Appointment.Requests;

public sealed record CreateAppointmentRequest(
    Guid DoctorId,
    Guid PatientId,
    Guid ClinicId,
    DateTime StartDateTime,
    string? Notes);
