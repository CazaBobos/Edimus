using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Features.CreateProduct;
using Products.Core.Features.GetProducts;
using Products.Core.Model;
using Products.Input;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Products.Controllers;

[ApiController]
//[Authorize]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all products that match the given name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetMany([FromQuery] GetProductsInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductsRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            Name = input.Name,
            Description = input.Description,
            Categories = input.Categories,
            MinPrice = input.MinPrice,
            MaxPrice = input.MaxPrice,
            Tags = input.Tags,
            Enabled = input.Enabled,
        }, cancellationToken);

        HttpContext.Response.Headers.Add(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.Add(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.Add(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Products);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateProductRequest
        {
            Parent = input.Parent,
            Category = input.Category,
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates an existing
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Create(int id, UpdateProductInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProductRequest
        {
            Id = id,
            Category = input.Category,
            Parent = input.Parent,
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}