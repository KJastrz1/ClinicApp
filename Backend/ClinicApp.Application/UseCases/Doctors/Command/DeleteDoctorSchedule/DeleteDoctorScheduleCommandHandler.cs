using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Command.DeleteDoctorSchedule;

internal sealed class DeleteDoctorScheduleCommandHandler : ICommandHandler<DeleteDoctorScheduleCommand>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorScheduleCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);

        if (doctor == null)
        {
            return Result.Failure(DoctorErrors.NotFound(doctorId));
        }

        DoctorScheduleId scheduleId = DoctorScheduleId.Create(request.ScheduleId).Value;
        Result<Doctor> result = doctor.RemoveSchedule(scheduleId);

        if (result.IsFailure)
        {
            return result; 
        }

        _doctorRepository.Update(doctor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
