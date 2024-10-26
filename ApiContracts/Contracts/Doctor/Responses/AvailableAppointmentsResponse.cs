using Shared.Contracts.Clinic.Responses;

namespace Shared.Contracts.Doctor.Responses;

public record AvailableAppointmentsResponse(
    ClinicResponse Clinic,
    List<DateTime> Dates
);
