using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Features.CreateProduct;
using Products.Core.Features.GetProductImage;
using Products.Core.Features.GetProducts;
using Products.Core.Features.UpdateProductImage;
using Products.Core.Features.RemoveProduct;
using Products.Core.Features.RestoreProduct;
using Products.Input;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Products.Controllers;

[ApiController]
[Authorize]
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
            User = User.Identity?.IsAuthenticated == true ? User.GetUser() : null,
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
    public async Task<IActionResult> Create([FromBody] CreateProductInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateProductRequest
        {
            ParentId = input.ParentId,
            CategoryId = input.CategoryId,
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProductRequest
        {
            Id = id,
            CategoryId = input.CategoryId,
            ParentId = input.ParentId,
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            Consumptions = input.Consumptions,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Sets or removes the image for a product. Send null to remove.
    /// </summary>
    [HttpPut("{id}/image")]
    public async Task<IActionResult> UpdateImage(int id, [FromBody] UpdateProductImageInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProductImageRequest
        {
            Id = id,
            Image = input.Image,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.ImageId);
    }

    /// <summary>
    /// Returns the image for a product
    /// </summary>
    [HttpGet("{id}/image")]
    [AllowAnonymous]
    public async Task<IActionResult> GetImage(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductImageRequest { Id = id }, cancellationToken);
        return File(response.BLOB, "image/jpeg");
    }

    /// <summary>
    /// Removes a product
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveProductRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Restores a product
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RestoreProductRequest
        {
            Id = id,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}