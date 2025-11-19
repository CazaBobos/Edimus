using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Extensions;
using Tables.Core.Features.CreateTable;
using Tables.Core.Features.GetManyTables;
using Tables.Core.Features.RemoveTable;
using Tables.Core.Features.UpdateTable;
using Tables.Input;

namespace Tables.Controllers;

[ApiController]
[Authorize]
[Route("api/tables")]
public class TablesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TablesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all tables that match the given criteria
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindMany([FromQuery] GetTablesInput input, CancellationToken cancellationToken)
    {
        // is User.Companies.Contains(CompanyId) then allow Get/Create
        // CompanyId: string

        var response = await _mediator.Send(new GetTablesRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            LayoutId = input.LayoutId,
            Status = input.Status,
            Enabled = input.Enabled,
            User = User.GetUser(),
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Tables);
    }

    /// <summary>
    /// Creates a new table
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTableInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateTableRequest
        {
            LayoutId = input.LayoutId,
            Status = input.Status,
            PositionX = input.PositionX,
            PositionY = input.PositionY,
            Surface = input.Surface,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a table
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTableInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateTableRequest
        {
            Id = id,
            Status = input.Status,
            PositionX = input.PositionX,
            PositionY = input.PositionY,
            Surface = input.Surface,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Sets a table status to Calling
    /// </summary>
    [AllowAnonymous]
    [HttpPut("{id}/call")]
    public async Task<IActionResult> Call(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateTableRequest
        {
            Id = id,
            Status = TableStatus.Calling,
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Sets a table status back to Occupied
    /// </summary>
    [AllowAnonymous]
    [HttpPut("{id}/reset")]
    public async Task<IActionResult> Reset(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateTableRequest
        {
            Id = id,
            Status = TableStatus.Occupied,
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Deletes a table
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveTableRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}