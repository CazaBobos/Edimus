using Ingredients.Core.Features.CreateIngredient;
using Ingredients.Core.Features.GetManyIngredients;
using Ingredients.Core.Features.RemoveIngredient;
using Ingredients.Core.Features.RestoreIngredient;
using Ingredients.Core.Features.UpdateIngredient;
using Ingredients.Input;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Ingredients.Controllers;

[ApiController]
[Authorize]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IngredientsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all ingredients that match the given name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> FindMany([FromQuery] GetIngredientsInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetIngredientsRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            Name = input.Name,
            MinStock = input.MinStock,
            MaxStock = input.MaxStock,
            MinAlert = input.MinAlert,
            MaxAlert = input.MaxAlert,
            Unit = input.Unit,
            Enabled = input.Enabled,
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Ingredients);
    }

    /// <summary>
    /// Creates a new ingredient
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIngredientInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateIngredientRequest
        {
            Name = input.Name,
            Stock = input.Stock,
            Alert = input.Alert,
            Unit = input.Unit,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a ingredient
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateIngredientInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateIngredientRequest
        {
            Id = id,
            Name = input.Name,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Removes a ingredient
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveIngredientRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Restores a ingredient
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RestoreIngredientRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}