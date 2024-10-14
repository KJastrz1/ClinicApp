using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Application.Actions.Doctors.Query.GetDoctorById;

public sealed record GetDoctorByIdQuery
(Guid DoctorId) : IQuery<DoctorResponse>;
