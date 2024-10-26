using Shared.Contracts.Appointment.Responses;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Doctor.Responses;
using System;
using ClinicApp.Domain.Enums;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class AppointmentReadModel
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ClinicId { get; set; }
    public DateTime StartDateTime { get; set; }
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public AppointmentStatusEnum Status { get; set; }
    public bool IsReminderSent { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal AppointmentResponse MapToResponse(
        DoctorResponse doctor,
        ClinicResponse clinic)
    {
        return new AppointmentResponse(
            Id: Id,
            PatientId: PatientId,
            Doctor: doctor,
            Clinic: clinic,
            Date: StartDateTime,
            AppointmentDuration: DurationMinutes,
            Notes: Notes ?? string.Empty
        );
    }
}
