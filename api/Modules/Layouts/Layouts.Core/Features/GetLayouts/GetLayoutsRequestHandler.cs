using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Layouts.Core.Extensions;
using Layouts.Core.Model;
using Shared.Core.Extensions;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.GetLayouts;

public class GetLayoutsRequestHandler : IRequestHandler<GetLayoutsRequest, GetLayoutsResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public GetLayoutsRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<GetLayoutsResponse> Handle(GetLayoutsRequest request, CancellationToken cancellationToken)
    {
        var query = _layoutsRepository.AsQueryable()
            .WherePremise(request.PremiseId)
            .WhereEnabled(request.Enabled);

        var count = await query.CountAsync(cancellationToken);

        var layouts = await query
            .Paginate(request.Limit, request.Page)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetLayoutsResponse
        {
            Count = count,
            Limit = request.Limit,
            Page = request.Page,
            Layouts = layouts.Adapt<List<LayoutModel>>()
        };
    }
}
