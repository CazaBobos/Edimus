using Mediator;

namespace Premises.Core.Features.RemovePremise;

public class RemovePremiseRequest : IRequest<RemovePremiseResponse>
{
    public int Id { get; set; }
}
