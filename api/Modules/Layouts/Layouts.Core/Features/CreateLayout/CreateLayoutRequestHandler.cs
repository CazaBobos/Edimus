using Mediator;
using Shared.Core.Entities;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.CreateLayout;

public class CreateLayoutRequestHandler : IRequestHandler<CreateLayoutRequest, CreateLayoutResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public CreateLayoutRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<CreateLayoutResponse> Handle(CreateLayoutRequest request, CancellationToken cancellationToken)
    {
        var layout = new Layout(request.PremiseId, request.Name, request.Height, request.Width);

        await _layoutsRepository.Add(layout, cancellationToken);

        return new CreateLayoutResponse { Id = layout.Id };
    }
}
