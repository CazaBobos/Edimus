using MediatR;
using Sectors.Core.Abstractions;
using Shared.Core.Entities;

namespace Sectors.Core.Features.CreateSector;

public class CreateSectorRequestHandler : IRequestHandler<CreateSectorRequest, CreateSectorResponse>
{
    private readonly ISectorsRepository _sectorsRepository;

    public CreateSectorRequestHandler(ISectorsRepository sectorsRepository)
    {
        _sectorsRepository = sectorsRepository;
    }

    public async Task<CreateSectorResponse> Handle(CreateSectorRequest request, CancellationToken cancellationToken)
    {
        var sector = new Sector(
            request.LayoutId,
            request.PositionX,
            request.PositionY,
            request.Name,
            request.Color,
            request.Surface.Select(c => (c.X, c.Y)).ToList()
        );

        await _sectorsRepository.Add(sector, cancellationToken);

        return new CreateSectorResponse
        {
            Id = sector.Id
        };
    }
}