using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts;
using Shared.Contracts.Doctor;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Application.Actions.Doctors.Query.GetDoctors;

public sealed record GetDoctorsQuery : IQuery<PagedItems<DoctorResponse>>
{
    public DoctorFilter Filter { get; init; } = new DoctorFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
