using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Premises.Core.Features.CreatePremise;
using Premises.Core.Features.GetPremises;
using Premises.Core.Features.RemovePremise;
using Premises.Core.Features.RestorePremise;
using Premises.Core.Features.UpdatePremise;
using Premises.Input;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Premises.Controllers;

[ApiController]
[Authorize]
[Route("api/premises")]
public class PremisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PremisesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets all premises matching the given filters
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindMany([FromQuery] GetPremisesInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPremisesRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            CompanyId = input.CompanyId,
            Enabled = input.Enabled,
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Premises);
    }

    /// <summary>
    /// Creates a new premise
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePremiseInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreatePremiseRequest
        {
            CompanyId = input.CompanyId,
            Name = input.Name,
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a premise
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePremiseInput input, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdatePremiseRequest
        {
            Id = id,
            Name = input.Name,
        }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Removes a premise
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemovePremiseRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Restores a removed premise
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RestorePremiseRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok();
    }
}
