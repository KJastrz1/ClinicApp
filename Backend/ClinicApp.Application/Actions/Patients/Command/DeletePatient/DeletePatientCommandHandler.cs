using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Patients.Command.DeletePatient;

internal sealed class DeletePatientCommandHandler : ICommandHandler<DeletePatientCommand, bool>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        PatientId patientId = PatientId.Create(request.PatientId).Value;

        Patient? patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null)
        {
            return Result.Failure<bool>(PatientErrors.NotFound(patientId));
        }

        patient.Delete();
        _patientRepository.Remove(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}
