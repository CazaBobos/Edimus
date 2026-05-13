using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Layouts.Core.Abstractions;

namespace Layouts.Core.Features.UpdateBoundaries;

public class UpdateBoundariesRequestHandler : IRequestHandler<UpdateBoundariesRequest, UpdateBoundariesResponse>
{
    private readonly ILayoutsRepository _layoutsRepository;

    public UpdateBoundariesRequestHandler(ILayoutsRepository layoutsRepository)
    {
        _layoutsRepository = layoutsRepository;
    }

    public async ValueTask<UpdateBoundariesResponse> Handle(UpdateBoundariesRequest request, CancellationToken cancellationToken)
    {
        var layout = await _layoutsRepository.AsQueryable()
            .Include(l => l.Boundaries)
            .FirstOrDefaultAsync(l => l.Id == request.LayoutId, cancellationToken);

        if (layout is null) throw new HttpNotFoundException();

        var newBoundaries = request.Boundaries
            .Select(b => new LayoutCoord(b.X, b.Y, layout.Id, b.Type))
            .ToList();

        layout.UpdateBoundaries(newBoundaries);

        await _layoutsRepository.Update(layout, cancellationToken);

        return new UpdateBoundariesResponse();
    }
}
