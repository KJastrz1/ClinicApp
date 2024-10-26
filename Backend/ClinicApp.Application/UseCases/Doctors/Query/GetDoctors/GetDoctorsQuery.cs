using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Query.GetDoctors;

public sealed record GetDoctorsQuery : IQuery<PagedItems<DoctorResponse>>
{
    public DoctorFilter Filter { get; init; } = new DoctorFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
