using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sectors.Core.Features.CreateSector;
using Sectors.Core.Features.GetManySectors;
using Sectors.Core.Features.RemoveSector;
using Sectors.Core.Features.UpdateSector;
using Sectors.Input;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Sectors.Controllers;

[ApiController]
[Authorize]
[Route("api/sectors")]
public class SectorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SectorsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all sectors that match the given criteria
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindMany([FromQuery] GetSectorsInput input, CancellationToken cancellationToken)
    {
        // is User.Companies.Contains(CompanyId) then allow Get/Create
        // CompanyId: string

        var response = await _mediator.Send(new GetSectorsRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            LayoutId = input.LayoutId,
            Name = input.Name,
            Enabled = input.Enabled,
            User = User.GetUser(),
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Sectors);
    }

    /// <summary>
    /// Creates a new sector
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSectorInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateSectorRequest
        {
            LayoutId = input.LayoutId,
            PositionX = input.PositionX,
            PositionY = input.PositionY,
            Name = input.Name,
            Color = input.Color,
            Surface = input.Surface,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a sector
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSectorInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateSectorRequest
        {
            Id = id,
            PositionX = input.PositionX,
            PositionY = input.PositionY,
            Name = input.Name,
            Color = input.Color,
            Surface = input.Surface,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Deletes a sector
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveSectorRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}