using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class AppointmentErrors
{
    public static Error NotFound(Guid id) => new(
        "Appointment.NotFound",
        $"Appointment with ID {id} was not found.");
    
    public static Error EmptyId => new(
        "Appointment.EmptyId",
        "Appointment ID is empty.");
    
    public static readonly Error AppointmentInPast = new(
        "Appointment.AppointmentInPast",
        "Cannot schedule an appointment in the past.");

    public static readonly Error NotesTooLong = new(
        "Appointment.NotesTooLong",
        "Appointment notes are too long.");

    public static class RoomErrors
    {
        public static readonly Error Required = new(
            "Appointment.RoomRequired",
            "Appointment room is required.");

        public static readonly Error TooLong = new(
            "Appointment.RoomTooLong",
            "Appointment room name is too long.");
    }
}
