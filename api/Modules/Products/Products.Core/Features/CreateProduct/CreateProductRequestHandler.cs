using MediatR;
using Products.Core.Abstractions;
using Shared.Core.Entities;

namespace Products.Core.Features.CreateProduct;
public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductsRepository _productsRepository;

    public CreateProductRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Parent,
            request.Category,
            request.Price,
            request.Name,
            request.Description
        );

        var existingProduct = await _productsRepository
            .FindOne(x => x.Id == product.Id || x.Name == product.Name, cancellationToken);

        if (existingProduct is not null)
            throw new InvalidOperationException("The product already exists");

        await _productsRepository.Add(product, cancellationToken);

        return new CreateProductResponse
        {
            Id = product.Id
        };
    }
}