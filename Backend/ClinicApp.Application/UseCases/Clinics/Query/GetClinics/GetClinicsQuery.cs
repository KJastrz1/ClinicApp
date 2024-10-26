using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Clinics.Query.GetClinics;

public sealed record GetClinicsQuery : IQuery<PagedItems<ClinicResponse>>
{
    public ClinicFilter Filter { get; init; } = new ClinicFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
