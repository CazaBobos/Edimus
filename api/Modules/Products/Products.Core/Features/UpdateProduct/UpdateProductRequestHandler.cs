using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Products.Core.Model;
using Shared.Core.Exceptions;

namespace Products.Core.Features.CreateProduct;
public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IProductsRepository _productsRepository;

    public UpdateProductRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async ValueTask<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        var existingProduct = await _productsRepository.AsQueryable()
            .Where(x => x.Id != product.Id && x.Name == product.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProduct is not null)
            throw new InvalidOperationException("The product name already exists");

        product.Update(
            request.ParentId,
            request.CategoryId,
            request.Price,
            request.Name,
            request.Description, 
            request.Consumptions?.Select(c => (c.IngredientId, c.Amount)).ToList());

        await _productsRepository.Update(product, cancellationToken);

        return new UpdateProductResponse
        {
            Product = product.Adapt<ProductModel>()
        };
    }
}