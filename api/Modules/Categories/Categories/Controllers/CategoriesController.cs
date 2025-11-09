using Categories.Core.Features.CreateCategory;
using Categories.Core.Features.GetManyCategories;
using Categories.Core.Features.RemoveCategory;
using Categories.Core.Features.RestoreCategory;
using Categories.Core.Features.UpdateCategory;
using Categories.Core.Model;
using Categories.Input;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Categories.Controllers;

[ApiController]
[Authorize]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all categories that match the given name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> FindMany([FromQuery] GetCategoriesInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoriesRequest
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

        return Ok(response.Categories);
    }

    /// <summary>
    /// Creates a new category
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateCategoryRequest
        {
            CompanyId = input.CompanyId,
            Name = input.Name,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a category
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCategoryRequest
        {
            Id = id,
            Name = input.Name,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Removes a category
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveCategoryRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Restores a category
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RestoreCategoryRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}