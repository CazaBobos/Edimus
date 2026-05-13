using Mediator;
using Shared.Core.Exceptions;
using Premises.Core.Abstractions;

namespace Premises.Core.Features.RestorePremise;

public class RestorePremiseRequestHandler : IRequestHandler<RestorePremiseRequest, RestorePremiseResponse>
{
    private readonly IPremisesRepository _premisesRepository;

    public RestorePremiseRequestHandler(IPremisesRepository premisesRepository)
    {
        _premisesRepository = premisesRepository;
    }

    public async ValueTask<RestorePremiseResponse> Handle(RestorePremiseRequest request, CancellationToken cancellationToken)
    {
        var premise = await _premisesRepository.GetById(request.Id, cancellationToken);

        if (premise is null) throw new HttpNotFoundException();

        premise.Restore();

        await _premisesRepository.Update(premise, cancellationToken);

        return new RestorePremiseResponse();
    }
}
