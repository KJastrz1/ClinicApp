using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Patients.Query.GetPatients;

public sealed record GetPatientsQuery : IQuery<PagedItems<PatientResponse>>
{
    public PatientFilter Filter { get; init; } = new PatientFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
