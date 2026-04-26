using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Products.Core.Features.GetProductImage;
public class GetProductImageRequestHandler : IRequestHandler<GetProductImageRequest, GetProductImageResponse>
{
    private readonly IProductsRepository _productsRepository;

    public GetProductImageRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async ValueTask<GetProductImageResponse> Handle(GetProductImageRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.AsQueryable()
            .Include(p => p.Image)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product?.Image is null) throw new HttpNotFoundException();

        return new GetProductImageResponse { BLOB = product.Image.BLOB };
    }
}
