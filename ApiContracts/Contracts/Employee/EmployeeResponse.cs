using ClinicApp.Domain.Enums;

namespace Shared.Contracts.Employee;

public record EmployeeResponse(
    Guid Id,
    string FirstName,
    string LastName,
    UserRole UserRole
);
