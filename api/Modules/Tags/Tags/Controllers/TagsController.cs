using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;
using Tags.Core.Features.CreateTag;
using Tags.Core.Features.GetTags;
using Tags.Core.Features.RemoveTag;
using Tags.Core.Features.RestoreTag;
using Tags.Core.Features.UpdateTag;
using Tags.Input;

namespace Tags.Controllers;

[ApiController]
[Authorize]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets all tags matching the given filters
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> FindMany([FromQuery] GetTagsInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTagsRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            CompanyId = input.CompanyId,
            Name = input.Name,
            Enabled = input.Enabled,
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Tags);
    }

    /// <summary>
    /// Creates a new tag
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTagInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateTagRequest
        {
            CompanyId = input.CompanyId,
            Name = input.Name,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a tag
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTagInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateTagRequest
        {
            Id = id,
            Name = input.Name,
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Removes a tag
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveTagRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Restores a removed tag
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RestoreTagRequest
        {
            Id = id,
        }, cancellationToken);

        return Ok(response);
    }
}
