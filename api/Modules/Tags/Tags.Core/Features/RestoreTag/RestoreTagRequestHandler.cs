using Mediator;
using Shared.Core.Exceptions;
using Shared.Core.Persistence;

namespace Tags.Core.Features.RestoreTag;

public class RestoreTagRequestHandler : IRequestHandler<RestoreTagRequest, RestoreTagResponse>
{
    private readonly ITagsRepository _tagsRepository;

    public RestoreTagRequestHandler(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<RestoreTagResponse> Handle(RestoreTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagsRepository.GetById(request.Id, cancellationToken);

        if (tag is null) throw new HttpNotFoundException();

        tag.Restore();

        await _tagsRepository.Update(tag, cancellationToken);

        return new RestoreTagResponse();
    }
}
