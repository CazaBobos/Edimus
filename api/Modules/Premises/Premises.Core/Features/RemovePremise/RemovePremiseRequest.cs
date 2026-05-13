using Mediator;
using Shared.Core.Abstractions;

namespace Premises.Core.Features.RemovePremise;

public class RemovePremiseRequest : IRequest<RemovePremiseResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
