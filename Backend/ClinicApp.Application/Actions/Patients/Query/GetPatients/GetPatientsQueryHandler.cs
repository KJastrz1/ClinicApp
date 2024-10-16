using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Shared;
using Shared.Contracts;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Patients.Query.GetPatients;

internal sealed class GetPatientsQueryHandler : IQueryHandler<GetPatientsQuery, PagedItems<PatientResponse>>
{
    private readonly IPatientReadDapperRepository _patientReadRepository;

    public GetPatientsQueryHandler(IPatientReadDapperRepository patientReadRepository)
    {
        _patientReadRepository = patientReadRepository;
    }

    public async Task<Result<PagedItems<PatientResponse>>> Handle(GetPatientsQuery request,
        CancellationToken cancellationToken)
    {
        PagedItems<PatientResponse> items = await _patientReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        return Result.Success(items);
    }
}
