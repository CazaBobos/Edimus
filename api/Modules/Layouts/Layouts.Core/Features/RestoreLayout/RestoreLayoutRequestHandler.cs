using Mediator;
using Shared.Core.Exceptions;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.RestoreLayout;

public class RestoreLayoutRequestHandler : IRequestHandler<RestoreLayoutRequest, RestoreLayoutResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public RestoreLayoutRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<RestoreLayoutResponse> Handle(RestoreLayoutRequest request, CancellationToken cancellationToken)
    {
        var layout = await _layoutsRepository.GetById(request.Id, cancellationToken);

        if (layout is null) throw new HttpNotFoundException();

        layout.Restore();

        await _layoutsRepository.Update(layout, cancellationToken);

        return new RestoreLayoutResponse();
    }
}
