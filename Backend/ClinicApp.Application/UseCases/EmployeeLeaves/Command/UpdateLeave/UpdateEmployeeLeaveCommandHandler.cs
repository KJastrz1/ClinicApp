using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.UpdateLeave;

internal sealed class UpdateEmployeeLeaveCommandHandler : ICommandHandler<UpdateEmployeeLeaveCommand>
{
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeLeaveCommandHandler(
        IEmployeeLeaveRepository employeeLeaveRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeLeaveRepository = employeeLeaveRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateEmployeeLeaveCommand request, CancellationToken cancellationToken)
    {
        EmployeeLeaveId employeeLeaveId = EmployeeLeaveId.Create(request.LeaveId).Value;

        EmployeeLeave? employeeLeave = await _employeeLeaveRepository.GetByIdAsync(employeeLeaveId, cancellationToken);

        if (employeeLeave == null)
        {
            return Result.Failure<Guid>(EmployeeLeaveErrors.NotFound(employeeLeaveId));
        }

        if (request.Reason != null)
        {
            employeeLeave.ChangeLeaveReason(LeaveReason.Create(request.Reason).Value);
        }

        if (request.StartDate.HasValue || request.EndDate.HasValue)
        {
            if (request.StartDate.HasValue)
            {
                if (request.EndDate.HasValue)
                {
                    if (request.StartDate.Value >= request.EndDate.Value)
                    {
                        return Result.Failure<Guid>(EmployeeLeaveErrors.EndBeforeStart);
                    }

                    employeeLeave.ChangeLeaveDates(
                        LeaveStartDate.Create(request.StartDate.Value).Value,
                        LeaveEndDate.Create(request.EndDate.Value).Value);
                }
                else
                {
                    employeeLeave.ChangeLeaveDates(
                        LeaveStartDate.Create(request.StartDate.Value).Value,
                        employeeLeave.EndDate);
                }
            }
            else if (request.EndDate.HasValue)
            {
                if (employeeLeave.StartDate.Value >= request.EndDate.Value)
                {
                    return Result.Failure<Guid>(EmployeeLeaveErrors.EndBeforeStart);
                }

                employeeLeave.ChangeLeaveDates(
                    employeeLeave.StartDate, LeaveEndDate.Create(request.EndDate.Value).Value);
            }
        }

        _employeeLeaveRepository.Update(employeeLeave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
