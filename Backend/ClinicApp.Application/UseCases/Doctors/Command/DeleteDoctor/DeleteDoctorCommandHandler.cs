using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Command.DeleteDoctor;

internal sealed class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor == null)
        {
            return Result.Failure<Guid>(DoctorErrors.NotFound(doctorId));
        }

        doctor.Delete();

        _doctorRepository.Remove(doctor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return doctor.Id.Value;
    }
}
