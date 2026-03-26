using Mapster;
using Ingredients.Core.Abstractions;
using Ingredients.Core.Extensions;
using Ingredients.Core.Model;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace Ingredients.Core.Features.GetManyIngredients;

public class GetIngredientsRequestHandler : IRequestHandler<GetIngredientsRequest, GetIngredientsResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public GetIngredientsRequestHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async ValueTask<GetIngredientsResponse> Handle(GetIngredientsRequest request, CancellationToken cancellationToken)
    {
        var query = _ingredientsRepository.AsQueryable()
            .WhereName(request.Name)
            .WhereStockRange(request.MinStock, request.MaxStock)
            .WhereAlertRange(request.MinAlert, request.MaxAlert)
            .WhereUnit(request.Unit)
            .WhereEnabled(request.Enabled);

        var ingredients = await query
            .Paginate(request.Limit, request.Page)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetIngredientsResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Ingredients = ingredients.Adapt<List<IngredientModel>>()
        };
    }
}