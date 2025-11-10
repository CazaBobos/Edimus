using AutoMapper;
using Ingredients.Core.Abstractions;
using Ingredients.Core.Extensions;
using Ingredients.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace Ingredients.Core.Features.GetManyIngredients;

public class GetIngredientsRequestHandler : IRequestHandler<GetIngredientsRequest, GetIngredientsResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMapper _mapper;

    public GetIngredientsRequestHandler(IIngredientsRepository ingredientsRepository, IMapper mapper)
    {
        _ingredientsRepository = ingredientsRepository;
        _mapper = mapper;
    }

    public async Task<GetIngredientsResponse> Handle(GetIngredientsRequest request, CancellationToken cancellationToken)
    {
        var query = _ingredientsRepository.AsQueryable()
            .WhereName(request.Name)
            .WhereStockRange(request.MinStock, request.MaxStock)
            .WhereAlertRange(request.MinAlert, request.MaxAlert)
            .WhereUnit(request.Unit)
            .WhereEnabled(request.Enabled);

        var ingredients = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetIngredientsResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Ingredients = _mapper.Map<List<IngredientModel>>(ingredients)
        };
    }
}