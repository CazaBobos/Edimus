using Mediator;

namespace Premises.Core.Features.RestorePremise;

public class RestorePremiseRequest : IRequest<RestorePremiseResponse>
{
    public int Id { get; set; }
}
