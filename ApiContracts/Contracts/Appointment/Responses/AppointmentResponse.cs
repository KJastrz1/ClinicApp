using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Doctor.Responses;

namespace Shared.Contracts.Appointment.Responses;

public record AppointmentResponse(
    Guid Id,
    Guid PatientId,
    DoctorResponse Doctor,
    ClinicResponse Clinic,
    DateTime Date,
    int AppointmentDuration,
    string Notes
);
