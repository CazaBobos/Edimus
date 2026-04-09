using Mediator;
using Shared.Core.Abstractions;

namespace Statistics.Core.Features.GetRotation;

public class GetRotationRequest : IRequest<GetRotationResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public IUserRecord? User { get; set; }
}
