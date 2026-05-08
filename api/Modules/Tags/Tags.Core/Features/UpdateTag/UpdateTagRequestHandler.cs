using Mediator;
using Shared.Core.Exceptions;
using Shared.Core.Persistence;

namespace Tags.Core.Features.UpdateTag;

public class UpdateTagRequestHandler : IRequestHandler<UpdateTagRequest, UpdateTagResponse>
{
    private readonly ITagsRepository _tagsRepository;

    public UpdateTagRequestHandler(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<UpdateTagResponse> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagsRepository.GetById(request.Id, cancellationToken);

        if (tag is null) throw new HttpNotFoundException();

        tag.Update(request.Name);

        await _tagsRepository.Update(tag, cancellationToken);

        return new UpdateTagResponse();
    }
}
