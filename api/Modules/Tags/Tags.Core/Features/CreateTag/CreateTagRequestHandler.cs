using Mediator;
using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Tags.Core.Features.CreateTag;

public class CreateTagRequestHandler : IRequestHandler<CreateTagRequest, CreateTagResponse>
{
    private readonly ITagsRepository _tagsRepository;

    public CreateTagRequestHandler(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async ValueTask<CreateTagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var existing = await _tagsRepository.FindOne(x => x.Name == request.Name, cancellationToken);

        if (existing is not null)
            throw new InvalidOperationException("A tag with this name already exists.");

        var tag = new Tag(request.Name);

        await _tagsRepository.Add(tag, cancellationToken);

        return new CreateTagResponse { Id = tag.Id };
    }
}
