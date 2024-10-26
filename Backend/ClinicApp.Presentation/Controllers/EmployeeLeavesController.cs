using ClinicApp.Application.UseCases.EmployeeLeaves.Command.ChangeLeaveStatus;
using ClinicApp.Application.UseCases.EmployeeLeaves.Command.CreateEmployeeLeave;
using ClinicApp.Application.UseCases.EmployeeLeaves.Command.DeleteLeave;
using ClinicApp.Application.UseCases.EmployeeLeaves.Command.UpdateLeave;
using ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaveById;
using ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaves;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.EmployeeLeave.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Presentation.Controllers;

[Route("api/employee-leaves")]
public sealed class EmployeeLeavesController : ApiController
{
    public EmployeeLeavesController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeLeave(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEmployeeLeaveByIdQuery(id);
        Result<EmployeeLeaveResponse> result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEmployeeLeaves(
        [FromQuery] EmployeeLeaveFilter filter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetEmployeeLeavesQuery
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        Result<PagedItems<EmployeeLeaveResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }


    [HttpPost]
    public async Task<IActionResult> CreateEmployeeLeave(
        [FromBody] CreateEmployeeLeaveRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateEmployeeLeaveCommand(
            request.EmployeeId,
            request.Reason,
            request.StartDate,
            request.EndDate);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Created(string.Empty, result.Value);
    }


    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeLeaveStatus(
        Guid id,
        [FromBody] ChangeLeaveStatusRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeLeaveStatusCommand(id, request.NewStatus);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateEmployeeLeave(
        Guid id,
        [FromBody] UpdateEmployeeLeaveRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateEmployeeLeaveCommand(
            id,
            request.Reason,
            request.StartDate,
            request.EndDate); 

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeLeave(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeLeaveCommand(id);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
