namespace ClinicApp.Infrastructure.Database.Constants;

internal static class TableNames
{
    internal const string UserProfiles = nameof(UserProfiles);
    
    internal const string Employees = nameof(Employees);
    
    internal const string Patients = nameof(Patients);
    
    internal const string Doctors = nameof(Doctors);
    
    internal const string Admins = nameof(Admins);
    
    internal const string Clinics = nameof(Clinics);

    internal const string Appointments = nameof(Appointments);
    
    internal const string EmployeeLeaves = nameof(EmployeeLeaves);

    internal const string OutboxMessages = nameof(OutboxMessages);

    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);
}
