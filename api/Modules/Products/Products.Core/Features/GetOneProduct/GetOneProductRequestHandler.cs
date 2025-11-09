using AutoMapper;
using MediatR;
using Products.Core.Abstractions;
using Products.Core.Model;
using Shared.Core.Exceptions;

namespace Products.Core.Features.GetOneProduct;
public class GetOneProductRequestHandler : IRequestHandler<GetOneProductRequest, GetOneProductResponse>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetOneProductRequestHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }
    public async Task<GetOneProductResponse> Handle(GetOneProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetById(request.Id, cancellationToken);
        
        if (product is null) throw new HttpNotFoundException();

        return new GetOneProductResponse
        {
            Product = _mapper.Map<ProductModel>(product)
        };
    }
}
