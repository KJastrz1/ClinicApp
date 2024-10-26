namespace Shared.Contracts.Appointment.Requests;

public sealed record UpdateAppointmentNotesRequest(
    Guid AppointmentId,
    string Notes);
