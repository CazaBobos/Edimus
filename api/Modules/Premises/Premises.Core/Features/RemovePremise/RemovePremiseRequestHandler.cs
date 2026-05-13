using Mediator;
using Shared.Core.Exceptions;
using Premises.Core.Abstractions;

namespace Premises.Core.Features.RemovePremise;

public class RemovePremiseRequestHandler : IRequestHandler<RemovePremiseRequest, RemovePremiseResponse>
{
    private readonly IPremisesRepository _premisesRepository;

    public RemovePremiseRequestHandler(IPremisesRepository premisesRepository)
    {
        _premisesRepository = premisesRepository;
    }

    public async ValueTask<RemovePremiseResponse> Handle(RemovePremiseRequest request, CancellationToken cancellationToken)
    {
        var premise = await _premisesRepository.GetById(request.Id, cancellationToken);

        if (premise is null) throw new HttpNotFoundException();

        premise.Remove();

        await _premisesRepository.Update(premise, cancellationToken);

        return new RemovePremiseResponse();
    }
}
