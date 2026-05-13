using Mediator;
using Shared.Core.Entities;
using Premises.Core.Abstractions;

namespace Premises.Core.Features.CreatePremise;

public class CreatePremiseRequestHandler : IRequestHandler<CreatePremiseRequest, CreatePremiseResponse>
{
    private readonly IPremisesRepository _premisesRepository;

    public CreatePremiseRequestHandler(IPremisesRepository premisesRepository)
    {
        _premisesRepository = premisesRepository;
    }

    public async ValueTask<CreatePremiseResponse> Handle(CreatePremiseRequest request, CancellationToken cancellationToken)
    {
        var premise = new Premise(request.CompanyId, request.Name);

        await _premisesRepository.Add(premise, cancellationToken);

        return new CreatePremiseResponse { Id = premise.Id };
    }
}
