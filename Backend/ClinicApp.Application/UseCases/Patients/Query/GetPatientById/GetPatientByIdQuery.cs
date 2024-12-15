using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Responses;

namespace ClinicApp.Application.UseCases.Patients.Query.GetPatientById;

public sealed record GetPatientByIdQuery
(Guid PatientId) : IQuery<PatientResponse>;
