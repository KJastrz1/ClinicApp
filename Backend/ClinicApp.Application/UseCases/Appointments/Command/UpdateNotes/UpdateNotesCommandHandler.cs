using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Appointments.Command.UpdateNotes;

internal sealed class UpdateNotesCommandHandler : ICommandHandler<UpdateNotesCommand, Guid>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNotesCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateNotesCommand request, CancellationToken cancellationToken)
    {
        AppointmentId appointmentId = AppointmentId.Create(request.AppointmentId).Value;
        Appointment? appointment = await _appointmentRepository.GetByIdAsync(appointmentId, cancellationToken);

        if (appointment is null)
        {
            return Result.Failure<Guid>(AppointmentErrors.NotFound(appointmentId));
        }

        Result<AppointmentNotes> notesResult = AppointmentNotes.Create(request.Notes);
        if (notesResult.IsFailure)
        {
            return Result.Failure<Guid>(notesResult.Error);
        }

        appointment.UpdateNotes(notesResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id.Value;
    }
}
