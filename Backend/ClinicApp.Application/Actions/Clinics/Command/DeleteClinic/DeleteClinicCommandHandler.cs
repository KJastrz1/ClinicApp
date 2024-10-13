using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.Actions.Clinics.Command.DeleteClinic;

internal sealed class DeleteClinicCommandHandler : ICommandHandler<DeleteClinicCommand, Guid>
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClinicCommandHandler(
        IClinicRepository clinicRepository,
        IUnitOfWork unitOfWork)
    {
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteClinicCommand request, CancellationToken cancellationToken)
    {
        ClinicId clinicId = ClinicId.Create(request.ClinicId).Value;
        Clinic? clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken);
        
        if (clinic == null)
        {
            return Result.Failure<Guid>(ClinicErrors.NotFound(clinicId));
        }

        _clinicRepository.Remove(clinic);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return clinic.Id.Value;
    }
}
