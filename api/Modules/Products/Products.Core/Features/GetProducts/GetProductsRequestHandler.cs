using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Core.Abstractions;
using Products.Core.Extensions;
using Products.Core.Model;
using Shared.Core.Extensions;

namespace Products.Core.Features.GetProducts;

public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductsRequestHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var query = _productsRepository.AsQueryable()
            .WhereName(request.Name)
            .WhereDescription(request.Description)
            .WhereCategories(request.Categories)
            .WherePriceRange(request.MinPrice, request.MaxPrice)
            .WhereTags(request.Tags)
            .WhereEnabled(request.Enabled);

        var products = await query
            .Paginate(request.Limit, request.Page)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetProductsResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Products = _mapper.Map<List<ProductModel>>(products),
        };
    }
}