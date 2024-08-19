using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Features.Patient.GetPatientById;

public sealed record GetPatientByIdQuery
(Guid PatientId) : IQuery<PatientResponse>;
