using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;
using Shared.Core.Persistence;
using Tags.Core.Extensions;
using Tags.Core.Model;

namespace Tags.Core.Features.GetTags;

public class GetTagsRequestHandler : IRequestHandler<GetTagsRequest, GetTagsResponse>
{
    private readonly ITagsRepository _tagsRepository;

    public GetTagsRequestHandler(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<GetTagsResponse> Handle(GetTagsRequest request, CancellationToken cancellationToken)
    {
        var query = _tagsRepository.AsQueryable()
            .WhereCompany(request.CompanyId)
            .WhereName(request.Name)
            .WhereEnabled(request.Enabled);

        var count = await query.CountAsync(cancellationToken);

        var tags = await query
            .Paginate(request.Limit, request.Page)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetTagsResponse
        {
            Count = count,
            Limit = request.Limit,
            Page = request.Page,
            Tags = tags.Adapt<List<TagModel>>()
        };
    }
}
