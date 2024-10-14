using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.Actions.Patients.Query.GetPatientById;
using ClinicApp.Application.Mappings;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Doctors.Query.GetDoctorById;

internal sealed class GetDoctorByIdQueryHandler
    : IQueryHandler<GetDoctorByIdQuery, DoctorResponse>
{
    private readonly IDoctorReadRepository _doctorReadRepository;

    public GetDoctorByIdQueryHandler(IDoctorReadRepository doctorReadRepository)
    {
        _doctorReadRepository = doctorReadRepository;
    }

    public async Task<Result<DoctorResponse>> Handle(
        GetDoctorByIdQuery request,
        CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorReadRepository.GetByIdAsync(
            doctorId,
            cancellationToken);

        if (doctor is null)
        {
            return Result.Failure<DoctorResponse>(DoctorErrors.NotFound(doctorId));
        }

        
        return doctor.ToResponse();
    }
}
