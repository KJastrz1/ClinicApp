using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Patient;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Patients.Query.GetPatients;

public sealed record GetPatientsQuery : IQuery<PagedItems<PatientResponse>>
{
    public PatientFilter Filter { get; init; } = new PatientFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
