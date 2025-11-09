using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Products.Core.Model;

namespace Products.Core.Features.CreateProduct;
public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public UpdateProductRequestHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);

        product.Update( 
            request.Parent,
            request.Category,
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
            Product = _mapper.Map<ProductModel>(product)
        };
    }
}