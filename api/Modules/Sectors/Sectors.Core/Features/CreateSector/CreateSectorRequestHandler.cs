using MediatR;
using Shared.Core.Entities;
using Sectors.Core.Abstractions;

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
            request.Surface
        );

        await _sectorsRepository.Add(sector, cancellationToken);

        return new CreateSectorResponse
        {
            Id = sector.Id
        };
    }
}