using MediatR;
using Sectors.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Sectors.Core.Features.RemoveSector;

public class RemoveSectorRequestHandler : IRequestHandler<RemoveSectorRequest, RemoveSectorResponse>
{
    private readonly ISectorsRepository _sectorsRepository;

    public RemoveSectorRequestHandler(ISectorsRepository sectorsRepository)
    {
        _sectorsRepository = sectorsRepository;
    }

    public async Task<RemoveSectorResponse> Handle(RemoveSectorRequest request, CancellationToken cancellationToken)
    {
        var table = await _sectorsRepository.GetById(request.Id);

        if (table is null) throw new HttpNotFoundException();

        await _sectorsRepository.Remove(table, cancellationToken);

        return new RemoveSectorResponse();
    }
}