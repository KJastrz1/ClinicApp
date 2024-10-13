using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Application.Actions.Clinics.Query.GetClinicById;

public sealed record GetClinicByIdQuery
(Guid ClinicId) : IQuery<ClinicResponse>;
