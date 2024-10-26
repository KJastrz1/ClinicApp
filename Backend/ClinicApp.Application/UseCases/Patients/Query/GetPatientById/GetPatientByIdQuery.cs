using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.UseCases.Patients.Query.GetPatientById;

public sealed record GetPatientByIdQuery
(Guid PatientId) : IQuery<PatientResponse>;
