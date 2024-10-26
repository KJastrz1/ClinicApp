using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class AppointmentErrors
{
    public static Error NotFound(AppointmentId id) => new(
        "Appointment.NotFound",
        $"Appointment with ID {id.Value} was not found.");

    public static Error EmptyId => new(
        "Appointment.EmptyId",
        "Appointment ID is empty.");

    public static Error DoctorIdRequired = new(
        "Appointment.DoctorIdRequired",
        "Doctor ID is required.");

    public static Error PatientIdRequired = new(
        "Appointment.PatientIdRequired",
        "Patient ID is required.");

    public static Error ClinicIdRequired = new(
        "Appointment.ClinicIdRequired",
        "Clinic ID is required.");

    public static class NotesErrors
    {
        public static readonly Error Empty = new(
            "Appointment.NotesEmpty",
            "Appointment notes are required.");


        public static readonly Error TooLong = new(
            "Appointment.NotesTooLong",
            "Appointment notes are too long.");
    }

    public static Error InvalidDuration = new(
        "Schedule.InvalidVisitDuration",
        $"Visit durationMinutes must be between {ScheduleVisitDuration.MinDurationMinutes} and {ScheduleVisitDuration.MaxDurationMinutes} minutes.");

    public static readonly Error AppointmentInPast = new(
        "Appointment.AppointmentInPast",
        "Cannot schedule an appointment in the past.");

    public static readonly Error AppointmentDateNotAvailable = new(
        "Appointment.AppointmentDateNotAvailable",
        "Appointment date does not match the doctor's schedule.");

    public static readonly Error AppointmentDateTaken = new(
        "Appointment.AppointmentDateTaken",
        "Appointment date is already taken.");
}
