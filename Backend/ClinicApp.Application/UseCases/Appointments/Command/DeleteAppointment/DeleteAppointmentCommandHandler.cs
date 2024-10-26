using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Appointments.Command.DeleteAppointment;

internal sealed class DeleteAppointmentCommandHandler : ICommandHandler<DeleteAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        AppointmentId appointmentId = AppointmentId.Create(request.AppointmentId).Value;
        Appointment? appointment = await _appointmentRepository.GetByIdAsync(appointmentId, cancellationToken);
        if (appointment == null)
        {
            return Result.Failure<Guid>(AppointmentErrors.NotFound(appointmentId));
        }

        appointment.Delete(); 

        _appointmentRepository.Remove(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id.Value;
    }
}
