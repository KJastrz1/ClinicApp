using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Doctors.Command.DeleteDoctor;

public sealed record DeleteDoctorCommand(Guid DoctorId) : ICommand<Guid>;
