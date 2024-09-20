using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Patients.Query.GetPatientById;

public sealed record GetPatientByIdQuery
(Guid PatientId) : IQuery<PatientResponse>;
