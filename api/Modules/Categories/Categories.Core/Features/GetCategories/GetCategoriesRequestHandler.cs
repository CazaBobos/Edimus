using Mapster;
using Categories.Core.Abstractions;
using Categories.Core.Extensions;
using Categories.Core.Model;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace Categories.Core.Features.GetManyCategories;

public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public GetCategoriesRequestHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async ValueTask<GetCategoriesResponse> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var query = _categoriesRepository.AsQueryable()
            .WhereCompany(request.CompanyId)
            .WhereName(request.Name)
            .WhereEnabled(request.Enabled);

        var categories = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetCategoriesResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Categories = categories.Adapt<List<CategoryModel>>()
        };
    }
}