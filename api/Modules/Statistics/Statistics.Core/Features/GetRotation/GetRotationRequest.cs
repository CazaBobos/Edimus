using Mediator;

namespace Statistics.Core.Features.GetRotation;

public class GetRotationRequest : IRequest<GetRotationResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
