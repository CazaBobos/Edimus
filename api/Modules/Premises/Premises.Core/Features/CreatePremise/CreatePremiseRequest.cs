using Mediator;
using Shared.Core.Abstractions;

namespace Premises.Core.Features.CreatePremise;

public class CreatePremiseRequest : IRequest<CreatePremiseResponse>
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public IUserRecord User { get; set; } = null!;
}
