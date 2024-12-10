using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.ChangeLeaveStatus;

internal sealed class ChangeLeaveStatusCommandHandler : ICommandHandler<ChangeLeaveStatusCommand>
{
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeLeaveStatusCommandHandler(
        IEmployeeLeaveRepository employeeLeaveRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeLeaveRepository = employeeLeaveRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangeLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        EmployeeLeaveId employeeLeaveId = EmployeeLeaveId.Create(request.LeaveId).Value;
        EmployeeLeave? employeeLeave = await _employeeLeaveRepository.GetByIdAsync(employeeLeaveId,
            cancellationToken);

        if (employeeLeave == null)
        {
            return Result.Failure(EmployeeLeaveErrors.NotFound(employeeLeaveId));
        }

        employeeLeave.ChangeLeaveStatus(request.NewStatus);

        _employeeLeaveRepository.Update(employeeLeave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
