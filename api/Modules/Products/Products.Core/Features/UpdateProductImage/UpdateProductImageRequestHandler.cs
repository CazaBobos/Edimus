using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Products.Core.Features.UpdateProductImage;

public class UpdateProductImageRequestHandler : IRequestHandler<UpdateProductImageRequest, UpdateProductImageResponse>
{
    private readonly IProductsRepository _productsRepository;

    public UpdateProductImageRequestHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async ValueTask<UpdateProductImageResponse> Handle(UpdateProductImageRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.AsQueryable()
            .Include(p => p.Image)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        product.UpdateImage(request.Image);

        await _productsRepository.SaveChanges(cancellationToken);

        return new UpdateProductImageResponse
        {
            ImageId = product.ImageId
        };
    }
}
