using MediatR;
using Products.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Products.Core.Features.RestoreProduct;
public class RestoreProductRequestHandler : IRequestHandler<RestoreProductRequest, RestoreProductResponse>
{
    private readonly IProductsRepository _productsRepository;

    public RestoreProductRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    public async Task<RestoreProductResponse> Handle(RestoreProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        product.Restore();
        await _productsRepository.Update(product, cancellationToken);

        return new RestoreProductResponse();
    }
}
