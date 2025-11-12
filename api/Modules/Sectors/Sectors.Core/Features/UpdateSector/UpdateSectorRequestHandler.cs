using MediatR;
using Shared.Core.Exceptions;
using Sectors.Core.Abstractions;

namespace Sectors.Core.Features.UpdateSector;

public class UpdateSectorRequestHandler : IRequestHandler<UpdateSectorRequest, UpdateSectorResponse>
{
    private readonly ISectorsRepository _sectorsRepository;

    public UpdateSectorRequestHandler(ISectorsRepository sectorsRepository)
    {
        _sectorsRepository = sectorsRepository;
    }

    public async Task<UpdateSectorResponse> Handle(UpdateSectorRequest request, CancellationToken cancellationToken)
    {
        var sector = await _sectorsRepository.GetById(request.Id);

        if (sector is null) throw new HttpNotFoundException();

        sector.Update(
            request.Name,
            request.Color,
            request.PositionX, 
            request.PositionY,
            request.Surface
        );

        await _sectorsRepository.Update(sector, cancellationToken);
    
        return new UpdateSectorResponse();
    }
}