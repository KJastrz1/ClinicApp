using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Patients.Command.DeletePatient;

public sealed record DeletePatientCommand(Guid PatientId) : ICommand<bool>;
