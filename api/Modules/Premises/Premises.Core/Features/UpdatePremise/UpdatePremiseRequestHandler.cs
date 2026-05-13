using Mediator;
using Shared.Core.Exceptions;
using Premises.Core.Abstractions;

namespace Premises.Core.Features.UpdatePremise;

public class UpdatePremiseRequestHandler : IRequestHandler<UpdatePremiseRequest, UpdatePremiseResponse>
{
    private readonly IPremisesRepository _premisesRepository;

    public UpdatePremiseRequestHandler(IPremisesRepository premisesRepository)
    {
        _premisesRepository = premisesRepository;
    }

    public async ValueTask<UpdatePremiseResponse> Handle(UpdatePremiseRequest request, CancellationToken cancellationToken)
    {
        var premise = await _premisesRepository.GetById(request.Id, cancellationToken);

        if (premise is null) throw new HttpNotFoundException();

        premise.Update(request.Name);

        await _premisesRepository.Update(premise, cancellationToken);

        return new UpdatePremiseResponse();
    }
}
