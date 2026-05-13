using Mediator;
using Shared.Core.Exceptions;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.UpdateLayout;

public class UpdateLayoutRequestHandler : IRequestHandler<UpdateLayoutRequest, UpdateLayoutResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public UpdateLayoutRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<UpdateLayoutResponse> Handle(UpdateLayoutRequest request, CancellationToken cancellationToken)
    {
        var layout = await _layoutsRepository.GetById(request.Id, cancellationToken);

        if (layout is null) throw new HttpNotFoundException();

        layout.Update(request.Name, request.Height, request.Width);

        await _layoutsRepository.Update(layout, cancellationToken);

        return new UpdateLayoutResponse();
    }
}
