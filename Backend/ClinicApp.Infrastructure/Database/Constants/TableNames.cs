namespace ClinicApp.Infrastructure.Database.Constants;

internal static class TableNames
{
    internal const string Accounts = nameof(Accounts);
    
    internal const string AccountRoles = nameof(AccountRoles);
    
    internal const string Users = nameof(Users);
    
    internal const string Patients = nameof(Patients);
    
    internal const string Doctors = nameof(Doctors);
    
    internal const string Admins = nameof(Admins);
    
    internal const string Clinics = nameof(Clinics);

    internal const string Appointments = nameof(Appointments);

    internal const string OutboxMessages = nameof(OutboxMessages);

    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);

    internal const string Roles = nameof(Roles);

    internal const string Permissions = nameof(Permissions);
    
    internal const string RolePermissions = nameof(RolePermissions);
}
