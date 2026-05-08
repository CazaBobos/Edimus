using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Products.Core.Model;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Shared.Core.Persistence;

namespace Products.Core.Features.CreateProduct;

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IProductsRepository _productsRepository;
    private readonly ITagsRepository _tagsRepository;

    public UpdateProductRequestHandler(IProductsRepository productsRepository, ITagsRepository tagsRepository)
    {
        _productsRepository = productsRepository;
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.AsQueryable()
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) throw new HttpNotFoundException();

        var existingProduct = await _productsRepository.AsQueryable()
            .Where(x => x.Id != product.Id && x.Name == request.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProduct is not null)
            throw new InvalidOperationException("The product name already exists");

        List<Tag>? tags = null;
        if (request.TagIds is not null)
            tags = await _tagsRepository.AsQueryable()
                .Where(t => request.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

        product.Update(
            request.ParentId,
            request.CategoryId,
            request.Price,
            request.Name,
            request.Description,
            tags,
            request.Consumptions?.Select(c => (c.IngredientId, c.Amount)).ToList());

        await _productsRepository.Update(product, cancellationToken);

        return new UpdateProductResponse
        {
            Product = product.Adapt<ProductModel>()
        };
    }
}