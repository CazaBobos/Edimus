using Mapster;
using Mediator;
using Products.Core.Abstractions;
using Products.Core.Model;
using Shared.Core.Exceptions;

namespace Products.Core.Features.GetOneProduct;
public class GetOneProductRequestHandler : IRequestHandler<GetOneProductRequest, GetOneProductResponse>
{
    private readonly IProductsRepository _productsRepository;

    public GetOneProductRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    public async ValueTask<GetOneProductResponse> Handle(GetOneProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        return new GetOneProductResponse
        {
            Product = product.Adapt<ProductModel>()
        };
    }
}
