using Mediator;
using Shared.Core.Exceptions;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.RemoveLayout;

public class RemoveLayoutRequestHandler : IRequestHandler<RemoveLayoutRequest, RemoveLayoutResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public RemoveLayoutRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<RemoveLayoutResponse> Handle(RemoveLayoutRequest request, CancellationToken cancellationToken)
    {
        var layout = await _layoutsRepository.GetById(request.Id, cancellationToken);

        if (layout is null) throw new HttpNotFoundException();

        layout.Remove();

        await _layoutsRepository.Update(layout, cancellationToken);

        return new RemoveLayoutResponse();
    }
}
