using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.CreateEmployeeLeave;

internal sealed class CreateEmployeeLeaveCommandHandler : ICommandHandler<CreateEmployeeLeaveCommand, Guid>
{
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeLeaveCommandHandler(
        IEmployeeLeaveRepository employeeLeaveRepository,
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeLeaveRepository = employeeLeaveRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeLeaveCommand request, CancellationToken cancellationToken)
    {
        UserId employeeId = UserId.Create(request.EmployeeId).Value;
        Employee? employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);
        if (employee is null)
        {
            return Result.Failure<Guid>(EmployeeErrors.NotFound(employeeId));
        }

        LeaveStartDate startDate = LeaveStartDate.Create(request.StartDate).Value;
        LeaveEndDate endDate = LeaveEndDate.Create(request.EndDate).Value;
        LeaveReason reason = LeaveReason.Create(request.Reason).Value;


        bool hasOverlappingLeaves = await _employeeLeaveRepository.HasOverlappingLeavesAsync(
            employeeId,
            startDate,
            endDate,
            cancellationToken);

        if (hasOverlappingLeaves)
        {
            return Result.Failure<Guid>(EmployeeLeaveErrors.OverlappingLeave);
        }

        var employeeLeave = EmployeeLeave.Create(
            EmployeeLeaveId.New(),
            employee,
            reason,
            startDate,
            endDate);

        _employeeLeaveRepository.Add(employeeLeave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return employeeLeave.Id.Value;
    }
}
