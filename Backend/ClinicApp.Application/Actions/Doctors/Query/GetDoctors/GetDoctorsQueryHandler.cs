using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.Mappings;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Shared;
using Shared.Contracts;
using Shared.Contracts.Doctor;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Application.Actions.Doctors.Query.GetDoctors;

internal sealed class GetDoctorsQueryHandler : IQueryHandler<GetDoctorsQuery, PagedItems<DoctorResponse>>
{
    private readonly IDoctorReadRepository _doctorReadRepository;

    public GetDoctorsQueryHandler(IDoctorReadRepository doctorReadRepository)
    {
        _doctorReadRepository = doctorReadRepository;
    }

    public async Task<Result<PagedItems<DoctorResponse>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        PagedItems<Doctor> items = await _doctorReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);
        
        var responseItems = new PagedItems<DoctorResponse>
        {
            Items = items.Items.Select(doctor => doctor.ToResponse()).ToList(),
            TotalCount = items.TotalCount,
            PageSize = items.PageSize,
            CurrentPage = items.CurrentPage
        };

        return Result.Success(responseItems);
    }
}
