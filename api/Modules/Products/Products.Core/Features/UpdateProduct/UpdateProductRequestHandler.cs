using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Products.Core.Model;

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

        product.Update(
            request.ParentId,
            request.CategoryId,
            request.Price,
            request.Name,
            request.Description);

        var existingProduct = await _productsRepository.AsQueryable()
            .Where(x => x.Id != product.Id && x.Name == product.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProduct is not null)
            throw new InvalidOperationException("The product name already exists");

        await _productsRepository.Update(product, cancellationToken);

        return new UpdateProductResponse
        {
            Product = product.Adapt<ProductModel>()
        };
    }
}