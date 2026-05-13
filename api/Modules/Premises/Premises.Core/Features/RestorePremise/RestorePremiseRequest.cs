using Mediator;
using Shared.Core.Abstractions;

namespace Premises.Core.Features.RestorePremise;

public class RestorePremiseRequest : IRequest<RestorePremiseResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
