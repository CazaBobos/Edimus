using Mediator;
using Shared.Core.Abstractions;

namespace Premises.Core.Features.UpdatePremise;

public class UpdatePremiseRequest : IRequest<UpdatePremiseResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IUserRecord User { get; set; } = null!;
}
