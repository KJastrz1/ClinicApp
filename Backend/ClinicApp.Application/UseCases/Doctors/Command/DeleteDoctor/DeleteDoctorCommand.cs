using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Doctors.Command.DeleteDoctor;

public sealed record DeleteDoctorCommand(Guid DoctorId) : ICommand<Guid>;
