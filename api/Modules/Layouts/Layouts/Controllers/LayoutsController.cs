using Layouts.Core.Features.CreateLayout;
using Layouts.Core.Features.GetLayouts;
using Layouts.Core.Features.RemoveLayout;
using Layouts.Core.Features.RestoreLayout;
using Layouts.Core.Features.UpdateBoundaries;
using Layouts.Core.Features.UpdateLayout;
using Layouts.Input;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;

namespace Layouts.Controllers;

[ApiController]
[Authorize]
[Route("api/layouts")]
public class LayoutsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LayoutsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets all layouts matching the given filters
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindMany([FromQuery] GetLayoutsInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetLayoutsRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            PremiseId = input.PremiseId,
            Enabled = input.Enabled,
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Layouts);
    }

    /// <summary>
    /// Creates a new layout
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLayoutInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateLayoutRequest
        {
            PremiseId = input.PremiseId,
            Name = input.Name,
            Height = input.Height,
            Width = input.Width,
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a layout's metadata (name, dimensions)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLayoutInput input, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateLayoutRequest
        {
            Id = id,
            Name = input.Name,
            Height = input.Height,
            Width = input.Width,
        }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Replaces the boundary grid of a layout
    /// </summary>
    [HttpPut("{id}/boundaries")]
    public async Task<IActionResult> UpdateBoundaries(int id, [FromBody] UpdateBoundariesInput input, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateBoundariesRequest
        {
            LayoutId = id,
            Boundaries = input.Boundaries.Select(b => (b.X, b.Y, b.Type)).ToList(),
        }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Removes a layout
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveLayoutRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Restores a removed layout
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RestoreLayoutRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok();
    }
}
