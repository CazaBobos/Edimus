using Mediator;
using Shared.Core.Exceptions;
using Tags.Core.Abstractions;

namespace Tags.Core.Features.RemoveTag;

public class RemoveTagRequestHandler : IRequestHandler<RemoveTagRequest, RemoveTagResponse>
{
    private readonly ITagsRepository _tagsRepository;

    public RemoveTagRequestHandler(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<RemoveTagResponse> Handle(RemoveTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagsRepository.GetById(request.Id, cancellationToken);

        if (tag is null) throw new HttpNotFoundException();

        tag.Remove();

        await _tagsRepository.Update(tag, cancellationToken);

        return new RemoveTagResponse();
    }
}
