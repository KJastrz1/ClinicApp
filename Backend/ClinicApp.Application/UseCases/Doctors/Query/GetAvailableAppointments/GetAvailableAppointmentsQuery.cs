using MediatR;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Application.UseCases.Doctors.Query.GetAvailableAppointments;

public sealed record GetAvailableAppointmentsQuery(
    Guid DoctorId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<List<AvailableAppointmentsResponse>>;
