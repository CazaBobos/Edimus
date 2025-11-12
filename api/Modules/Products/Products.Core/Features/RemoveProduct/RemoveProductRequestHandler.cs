using MediatR;
using Products.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Products.Core.Features.RemoveProduct;
public class RemoveProductRequestHandler : IRequestHandler<RemoveProductRequest, RemoveProductResponse>
{
    private readonly IProductsRepository _productsRepository;

    public RemoveProductRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    public async Task<RemoveProductResponse> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        product.Remove();
        await _productsRepository.Update(product, cancellationToken);

        return new RemoveProductResponse();
    }
}
