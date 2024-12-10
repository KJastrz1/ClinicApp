using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.DeleteLeave;

internal sealed class DeleteEmployeeLeaveCommandHandler : ICommandHandler<DeleteEmployeeLeaveCommand, Guid>
{
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeLeaveCommandHandler(
        IEmployeeLeaveRepository employeeLeaveRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeLeaveRepository = employeeLeaveRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteEmployeeLeaveCommand request, CancellationToken cancellationToken)
    {
        EmployeeLeaveId leaveId = EmployeeLeaveId.Create(request.LeaveId).Value;
        EmployeeLeave? employeeLeave = await _employeeLeaveRepository.GetByIdAsync(leaveId, cancellationToken);
        
        if (employeeLeave == null)
        {
            return Result.Failure<Guid>(EmployeeLeaveErrors.NotFound(leaveId));
        }

        employeeLeave.Delete();

        _employeeLeaveRepository.Remove(employeeLeave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return employeeLeave.Id.Value;
    }
}
