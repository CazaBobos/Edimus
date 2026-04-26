using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Shared.Core.Exceptions;
using Shared.Core.Persistence;

namespace Products.Core.Features.UpdateProductImage;

public class UpdateProductImageRequestHandler : IRequestHandler<UpdateProductImageRequest, UpdateProductImageResponse>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IImageRepository _imageRepository;

    public UpdateProductImageRequestHandler(IProductsRepository productsRepository, IImageRepository imageRepository)
    {
        _productsRepository = productsRepository;
        _imageRepository = imageRepository;
    }

    public async ValueTask<UpdateProductImageResponse> Handle(UpdateProductImageRequest request, CancellationToken cancellationToken)
    {
        // Includes Image in the same query due to internal usage.
        // Prevents extra querying during execution.
        var product = await _productsRepository.AsQueryable()
            .Include(p => p.Image)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        product.UpdateImage(request.Image);

        var oldImage = product.Image;
        if (request.Image is null && oldImage is not null)
        {
            _imageRepository.MarkDeleted(oldImage);
        }

        await _productsRepository.SaveChanges(cancellationToken);

        return new UpdateProductImageResponse
        {
            ImageId = product.ImageId
        };
    }
}
